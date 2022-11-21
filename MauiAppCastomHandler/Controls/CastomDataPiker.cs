using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using static MauiAppCastomHandler.Controls.CastomDataPiker;

namespace MauiAppCastomHandler.Controls;


public class CastomDataPiker : ContentView
{
    ImageButton pikerImage;
    ImageButton clearImage;
    public string emptyDataPikerId;
    public CastomDataPiker()
	{
	    pikerImage = new ImageButton()
            .Source("dotnet_bot.png")
            .Size(10,10);
	
        clearImage = new ImageButton()
            .Source("dotnet_bot.png")
            .Size(10, 10);
        var emptyDataPiker = new EmptyDataPiker(this);
       
        pikerImage.Clicked += PikerImage_Clicked;
        emptyDataPiker.EmptyChanged += EmptyDataPiker_EmptyChanged;
       
        clearImage.Clicked += ClearImage_Clicked;
       

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
           // pikerImage.IsVisible=IsEmpty;
            clearImage.IsVisible=!IsEmpty;
        }
      
        void PikerImage_Clicked(object sender, EventArgs e)
        {
            
        }
        void ClearImage_Clicked(object sender, EventArgs e)
        {
            emptyDataPiker.IsEmpty = true;
        }
    }

   
    public class EmptyDataPiker : DatePicker
    {
        private CastomDataPiker castomPieker;
        private bool isEmpty;
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
}