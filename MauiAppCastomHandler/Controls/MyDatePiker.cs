using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiAppCastomHandler.Controls.CastomDataPiker;

namespace MauiAppCastomHandler.Controls
{
    public class MyDatePieker : ContentView
    {
        public DateTime LostSelectDate { get; private set; } = DateTime.Now.Date;

        ImageButton pikerImage;
        ImageButton clearImage;
        MyEmpryDataPiker emptyDataPiker;
        public MyDatePieker()
        {
            pikerImage = new ImageButton()
                .Source("dotnet_bot.png")
                .Size(10, 10);
            pikerImage.IsVisible = false;

            clearImage = new ImageButton()
                .Source("dotnet_bot.png")
                .Size(10, 10);
            clearImage.IsVisible = false;


            emptyDataPiker = new MyEmpryDataPiker(this);
            emptyDataPiker.MaximumDate = DateTime.Now;

            pikerImage.Clicked += PikerImage_Clicked;
            clearImage.Clicked += ClearImage_Clicked;

            emptyDataPiker.DateSelected += EmptyDataPiker_DateSelected;

            Content = new HorizontalStackLayout
            {
                Children =
                {
                    // pikerImage,
                    emptyDataPiker,
                    clearImage
                }
            };
            void PikerImage_Clicked(object sender, EventArgs e)
            {

            }
            void ClearImage_Clicked(object sender, EventArgs e)
            {
                DateValue = new DateTime(1900, 1, 1);
            }
            void EmptyDataPiker_DateSelected(object sender, DateChangedEventArgs e)
            {

                if (DateValue == e.NewDate)
                    return;
                if (e.NewDate <= new DateTime(1900, 1, 1))
                {
                    DateValue = new DateTime(1900, 1, 1);
                }
                else
                    DateValue = e.NewDate;
            }
        }


        #region MinDateValue
        public static readonly BindableProperty MinDateValueProperty =
           BindableProperty.Create("MinDateValue", typeof(DateTime), typeof(MyDatePieker), DateTime.MinValue, propertyChanged: OnMinDateValueChanged);
        private static void OnMinDateValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((DateTime)newValue <= new DateTime(1900, 1, 1))
                (bindable as MyDatePieker).emptyDataPiker.MinimumDate = new DateTime(1900, 1, 1);
            else
                (bindable as MyDatePieker).emptyDataPiker.MinimumDate = ((DateTime)newValue).Date;
        }
        public DateTime MinDateValue
        {
            get => (DateTime)GetValue(MinDateValueProperty);
            set => SetValue(MinDateValueProperty, value);
        }
        #endregion

        #region MaxDateValue
        public static readonly BindableProperty MaxDateValueProperty =
           BindableProperty.Create("MaxDateValue", typeof(DateTime), typeof(MyDatePieker), DateTime.MaxValue, propertyChanged: OnMaxDateValueChanged);

        private static void OnMaxDateValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //(bindable as CastomDataPiker).emptyDataPiker.MaximumDate = (DateTime)newValue;

            //if ((DateTime)newValue >= DateTime.MaxValue)
            //    (bindable as CastomDataPiker).emptyDataPiker.MaximumDate = DateTimeOffset.MaxValue.UtcDateTime;
            //else
            //    (bindable as CastomDataPiker).emptyDataPiker.MaximumDate = (DateTime)newValue;
        }

        public DateTime MaxDateValue
        {
            get => (DateTime)GetValue(MaxDateValueProperty);
            set => SetValue(MaxDateValueProperty, value);
        }
        #endregion

        #region DateValue
        public static readonly BindableProperty DateValueProperty =
           BindableProperty.Create("DateValue", typeof(DateTime), typeof(MyDatePieker), new DateTime(1900, 1, 1), BindingMode.OneWay, propertyChanged: OnDateValueChanged);
        private static void OnDateValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var nval = (DateTime)newValue;
            if (nval <= new DateTime(1900, 1, 1))
            {
                (bindable as MyDatePieker).clearImage.IsVisible = false;
            }
            else
            {
                (bindable as MyDatePieker).LostSelectDate = nval;
                (bindable as MyDatePieker).clearImage.IsVisible = true;
            }
        }
        public DateTime DateValue
        {
            get
            {
                var curentDate = (DateTime)GetValue(DateValueProperty);
                return curentDate;
            }
               
            set => SetValue(DateValueProperty, value);
        }
        #endregion

        //#region IsNotEmpty

        //public static readonly BindableProperty IsNotEmptyProperty =
        //   BindableProperty.Create("IsNotEmpty", typeof(bool), typeof(MyDatePieker), false, BindingMode.OneWay, propertyChanged: OnIsNotEmptyChanged);

        //private static void OnIsNotEmptyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    //(bindable as CastomDataPiker).emptyDataPiker.IsEmpty = (bool)newValue;
        //}

        //public bool IsNotEmpty
        //{
        //    get => (bool)GetValue(IsNotEmptyProperty);
        //    set => SetValue(IsNotEmptyProperty, value);
        //}
        //#endregion
    }
}
