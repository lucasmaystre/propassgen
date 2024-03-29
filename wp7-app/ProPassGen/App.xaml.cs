﻿/**
 * Copyright (C) 2011 Lucas Maystre <lucas@maystre.ch>
 *
 * This file is part of propassgen.
 *
 * propassgen is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * propassgen is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with propassgen.  If not, see <http://www.gnu.org/licenses/>
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Resources;
using System.Xml.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ProPassGen
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        // Related to application state.
        public List<Char> symbols { get; private set; }
        public Dictionary<string, int> frequencies { get; private set; }
        public int total { get; private set; }
        public float entropy { get; private set; }

        public event EventHandler ApplicationDataChanged;

        // Method to raise the ApplicationDataChanged event.
        protected void OnApplicationDataChanged(EventArgs e)
        {
            EventHandler handler = ApplicationDataChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        private void GetData()
        {
            // Read xml source file, and parse it in a more application-friendly data structure.
            // Declare an XElement to hold the contents of the xml file. 
            XElement sourceXml;

            // Get a stream to the XML file which contains the data and load it into the XElement. 
            StreamResourceInfo xml = Application.GetResourceStream(
                new Uri("/ProPassGen;component/source.xml", UriKind.Relative));
            sourceXml = XElement.Load(xml.Stream);
            entropy = float.Parse(sourceXml.Attribute("entropy").Value,
                    CultureInfo.InvariantCulture.NumberFormat); // We use a dot as decimal separator.

            frequencies = new Dictionary<string, int>();
            symbols = new List<Char>();

            XElement symbs = sourceXml.Element("symbols");
            foreach (var symb in symbs.Elements("symbol"))
            {
                // It is only one char long.
                symbols.Add(symb.Attribute("value").Value[0]);
            }

            XElement seqs = sourceXml.Element("sequences");
            foreach (var seq in seqs.Elements("sequence"))
            {
                string trig = seq.Attribute("value").Value;
                int freq = int.Parse(seq.Attribute("frequency").Value);
                frequencies[trig] = freq;
                total += freq;
            }
            // Notify that the data is ready to be used.
            OnApplicationDataChanged(EventArgs.Empty);
        }

        public void GetDataAsync()
        {
            // Call the GetData method on a new thread.
            Thread t = new Thread(new ThreadStart(GetData));
            t.Start();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
