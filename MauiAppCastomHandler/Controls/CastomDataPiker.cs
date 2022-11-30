using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
#if WINDOWS10_0_19041_0_OR_GREATER
using Microsoft.UI.Xaml.Controls;
#endif
using static MauiAppCastomHandler.Controls.CastomDataPiker;
using DatePicker = Microsoft.Maui.Controls.DatePicker;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace MauiAppCastomHandler.Controls;


public class CastomDataPiker : ContentView
{
    #region MinDateValue
    public static readonly BindableProperty MinDateValueProperty =
       BindableProperty.Create("MinDateValue", typeof(DateTime), typeof(CastomDataPiker), DateTime.MinValue, propertyChanged: OnMinDateValueChanged);
    private static void OnMinDateValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if((DateTime)newValue <= DateTime.MinValue)
            (bindable as CastomDataPiker).emptyDataPiker.MinimumDate = DateTimeOffset.MinValue.UtcDateTime;
        else
            (bindable as CastomDataPiker).emptyDataPiker.MinimumDate = (DateTime)newValue;
    }
    public DateTime MinDateValue
    {
        get => (DateTime)GetValue(MinDateValueProperty);
        set => SetValue(MinDateValueProperty, value);
    }
    #endregion

    #region MaxDateValue
    public static readonly BindableProperty MaxDateValueProperty =
       BindableProperty.Create("MaxDateValue", typeof(DateTime), typeof(CastomDataPiker), DateTime.MaxValue, propertyChanged: OnMaxDateValueChanged);

    private static void OnMaxDateValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as CastomDataPiker).emptyDataPiker.MaximumDate = (DateTime)newValue;

        if ((DateTime)newValue >= DateTime.MaxValue)
            (bindable as CastomDataPiker).emptyDataPiker.MaximumDate = DateTimeOffset.MaxValue.UtcDateTime;
        else
            (bindable as CastomDataPiker).emptyDataPiker.MaximumDate = (DateTime)newValue;
    }

    public DateTime MaxDateValue
    {
        get => (DateTime)GetValue(MaxDateValueProperty);
        set => SetValue(MaxDateValueProperty, value);
    }
    #endregion

    #region DateValue
    public static readonly BindableProperty DateValueProperty =
       BindableProperty.Create("DateValue", typeof(DateTime), typeof(CastomDataPiker), DateTime.MinValue, BindingMode.OneWay, propertyChanged: OnDateValueChanged);
    private static void OnDateValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var castomPieker = (CastomDataPiker)bindable;
        var xxx= (DateTime)newValue;
        castomPieker.emptyDataPiker.Date = (DateTime)newValue;
        if ((DateTime)newValue <= new DateTime(1900, 1, 1))
        {
            castomPieker.emptyDataPiker.IsEmpty = true;
        }
        else
            (bindable as CastomDataPiker).emptyDataPiker.IsEmpty = false;
    }
    public DateTime DateValue
    {
        get => (DateTime)GetValue(DateValueProperty);
        set => SetValue(DateValueProperty, value);
           
        
    }
    #endregion

    #region IsNotEmpty

    public static readonly BindableProperty IsNotEmptyProperty =
       BindableProperty.Create("IsNotEmpty", typeof(bool), typeof(CastomDataPiker), false, BindingMode.OneWay, propertyChanged: OnIsNotEmptyChanged);

    private static void OnIsNotEmptyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //(bindable as CastomDataPiker).emptyDataPiker.IsEmpty = (bool)newValue;
    }

    public bool IsNotEmpty
    {
        get => (bool)GetValue(IsNotEmptyProperty);
        set => SetValue(IsNotEmptyProperty, value);
    }
    #endregion

    ImageButton pikerImage;
    ImageButton clearImage;
    EmptyDataPiker emptyDataPiker;
    public string emptyDataPikerId;
    public CastomDataPiker()
	{
	    pikerImage = new ImageButton()
            .Source("dotnet_bot.png")
            .Size(10,10);
        pikerImage.IsVisible = false;
	
        clearImage = new ImageButton()
            .Source("dotnet_bot.png")
            .Size(10, 10);
        clearImage.IsVisible = false;
        

        emptyDataPiker = new EmptyDataPiker(this);
        emptyDataPiker.MaximumDate=DateTime.Now;
       
        pikerImage.Clicked += PikerImage_Clicked;
        emptyDataPiker.EmptyChanged += EmptyDataPiker_EmptyChanged;
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
        emptyDataPikerId = emptyDataPiker.Id.ToString();


        void EmptyDataPiker_EmptyChanged(object s, bool IsEmpty)
        {
            this.IsNotEmpty=!IsEmpty;
           // pikerImage.IsVisible=IsEmpty;
            clearImage.IsVisible=!IsEmpty;
            if (IsEmpty)
                DateValue = DateTime.MinValue;
        }
        void PikerImage_Clicked(object sender, EventArgs e)
        {
            
        }
        void ClearImage_Clicked(object sender, EventArgs e)
        {
            emptyDataPiker.IsEmpty = true;
        }
        void EmptyDataPiker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(DateValue == e.NewDate)
                return;
            if (e.NewDate <= new DateTime(1900, 1, 1))
            {
                DateValue = DateTime.MinValue;
            }
            else
                DateValue = e.NewDate; 
        }
    }



    public class EmptyDataPiker : DatePicker
    {
        private CastomDataPiker castomPieker;
        private bool isEmpty =true;
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }
            set
            {
                if (isEmpty != value)
                {
                    isEmpty = value;
                    EmptyChanged.Invoke(this, isEmpty);
                }
            }
        }
        public delegate void EmptyHandler(object sender, bool IsEmpty);
        public event EmptyHandler EmptyChanged;
        public EmptyDataPiker(CastomDataPiker context) : base()
        {
            castomPieker = context;
        }

    }




    //public class EmptyDataPiker : DatePicker
    //{
    //    private CastomDataPiker castomPieker;
    //    private bool isEmpty;
    //    public bool IsEmpty
    //    {
    //        get
    //        {
    //            return isEmpty;
    //        }
    //        set 
    //        {
    //            if (isEmpty != value)
    //            {
    //                isEmpty = value;
    //                EmptyChanged.Invoke(this, isEmpty);
    //            }
    //        } 
    //    }


    //    public delegate void EmptyHandler(object sender, bool IsEmpty);
    //    public event EmptyHandler EmptyChanged;

    //    public EmptyDataPiker(CastomDataPiker context) : base()
    //    {
    //        castomPieker = context;
    //    }
    //}
}