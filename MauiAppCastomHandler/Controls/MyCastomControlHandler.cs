using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiAppCastomHandler.Controls.CastomDataPiker;
#if WINDOWS
using Microsoft.UI.Xaml.Controls;
#endif

namespace MauiAppCastomHandler.Controls
{
    public class MyCastomControlHandler
    {
        public MyCastomControlHandler()
        {
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("MyCastomControlHandler", (handler, view) =>
            {
                if (view is MyEmpryDataPiker)
                {
#if ANDROID
                    var datePiker = (MyEmpryDataPiker)view;
                    var dateTextBox= handler.PlatformView as Android.Widget.TextView;

                    datePiker.Loaded += (s, e) =>
                    {
                        dateTextBox.Text = "Choose date";
                    };
                    datePiker.MyDatePieker.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(MyDatePieker.DateValue))
                        {
                            if((s as MyDatePieker).DateValue<= new DateTime(1900, 1, 1))
                                dateTextBox.Text = "Choose date";
                        }
                    };
                    dateTextBox.Click += (s, e) =>
                    {
                        MauiDatePicker mauiDatePicker = (MauiDatePicker)s;
                        mauiDatePicker.ShowPicker.Invoke();
                        if (datePiker.MyDatePieker.DateValue <= new DateTime(1900, 1, 1))
                        {
                            datePiker.MyDatePieker.DateValue = datePiker.MyDatePieker.LostSelectDate;
                            dateTextBox.Text = datePiker.MyDatePieker.LostSelectDate.ToShortDateString();
                        }
                    };
#endif

#if WINDOWS
                    var datePiker = (MyEmpryDataPiker)view;
                    var dateTextBox = handler.PlatformView as CalendarDatePicker;

                    datePiker.Loaded += (s, e) =>
                    {
                        dateTextBox.Date = null;
                    };
                    datePiker.MyDatePieker.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(MyDatePieker.DateValue))
                        {
                            if ((s as MyDatePieker).DateValue <= new DateTime(1900, 1, 1))
                                dateTextBox.Date = null;
                        }
                    };
                    dateTextBox.Closed += (s, e) =>
                    {
                        if (dateTextBox.Date == DateTime.Now.Date)
                            datePiker.MyDatePieker.DateValue = DateTime.Now;
                    };
#endif
                }
            });
        }

        
    }
}
         