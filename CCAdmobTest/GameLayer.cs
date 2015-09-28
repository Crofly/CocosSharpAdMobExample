using System;
using System.Collections.Generic;
using CocosSharp;
using Android.App;
using Android.Views;
using Android.Gms.Ads;

namespace CCAdmobTest
{
	public class GameLayer : CCLayer
	{
		private Activity activity;

		private float playTime = 0.0f;

		public GameLayer (Activity activity)
		{
			this.activity = activity;
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			// Use the bounds to layout the positioning of our drawable assets
			CCRect bounds = VisibleBoundsWorldspace;

			// Register for touch events
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesEnded = OnTouchesEnded;
			AddEventListener (touchListener, this);


			Android.Graphics.Point size = new Android.Graphics.Point();
			activity.WindowManager.DefaultDisplay.GetSize(size);
			ViewGroup.LayoutParams adParams = new ViewGroup.LayoutParams(
				size.X,                        
				100);

			var ad = new AdView (activity);
			ad.AdSize = AdSize.SmartBanner;
			ad.AdUnitId = "YOUR ADD ID HERE";
			ad.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			var requestbuilder = new AdRequest.Builder ();
			ad.LoadAd (requestbuilder.Build ());

			activity.AddContentView(ad,adParams);
			ad.BringToFront ();

			Schedule (t => {
				playTime += t;
				if (playTime > 3 ) {
					ad.BringToFront();
				}
			}, 1.0f);

		}

		void OnTouchesEnded (List<CCTouch> touches, CCEvent touchEvent)
		{
			if (touches.Count > 0) {
				// Perform touch handling here
			}
		}
	}
}
