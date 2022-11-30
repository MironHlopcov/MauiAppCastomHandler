using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiAppCastomHandler.Controls.CastomDataPiker;

namespace MauiAppCastomHandler.Controls
{
    public class CastomControlHandlers
    {
        public CastomControlHandlers() 
        {
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            { 
                if (view is EmptyDataPiker)
                {
#if ANDROID
                    var epdp = ((EmptyDataPiker)view);
                    var x2 = handler.PlatformView as Android.Widget.TextView;
                    x2.SetBackgroundColor(Android.Graphics.Color.Transparent);
                  
                    x2.Click += X2_Click;
                    epdp.Loaded += (s, e) =>
                    {
                        x2.Text = "Choose date";
                    };
                    epdp.EmptyChanged += (s, v) =>
                    {
                        if (v == true)
                        {
                            x2.Text = "Choose date";
                        }
                    };

                    void X2_Click(object sender, EventArgs e)
                    {
                        if (x2.Text == "Choose date")
                            epdp.Date = DateTime.Now;
                        MauiDatePicker c = (MauiDatePicker)sender;
                        c.ShowPicker.Invoke();
                        x2.Text = DateTime.Now.ToShortDateString();
                    }

#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
#elif WINDOWS
                var x1 = handler.PlatformView as Microsoft.UI.Xaml.Controls.CalendarDatePicker;
                if (x1.Tag == null)
                {

                    x1.Tag = ((EmptyDataPiker)view).Id;
                    x1.DateChanged += (s, e) =>
                    {
                        var epdp = ((EmptyDataPiker)view);
                        if (x1.Date != null)
                            epdp.IsEmpty = false;
                        else
                            epdp.IsEmpty = true;

                    };
                    x1.Loaded += (s, e) =>
                    {
                      x1.Date = null;
                    };

                    ((EmptyDataPiker)view).EmptyChanged += (s, v) =>
                    {
                        //if (x1.Tag.ToString() != ((EmptyDataPiker)s).Id.ToString())
                        //    return;
                        if (v == true)
                            x1.Date = null;
                    };
                }
#endif
                }
            });
        }
    }
}
