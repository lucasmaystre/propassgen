﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProPassGen {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ProPassGen.AppResources", typeof(AppResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to How does it work behind the scenes? The password generator models the English language as a second-order Markov source. The probability distribution is generated from statistics on a set of English books..
        /// </summary>
        public static string AdvancedExplanation {
            get {
                return ResourceManager.GetString("AdvancedExplanation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ProPassGen 1.0.
        /// </summary>
        public static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This program generates passwords that are designed to be somehwat similar to English words. They are of relatively low entropy, meaning that they are not very strong and shouldn&apos;t be used in critical places..
        /// </summary>
        public static string BasicExplanation {
            get {
                return ResourceManager.GetString("BasicExplanation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Copyright © 2011 Lucas Maystre.
        /// </summary>
        public static string Copyright {
            get {
                return ResourceManager.GetString("Copyright", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Credit goes to the following people / resources:
        ///- T. Van Vleck&apos;s algorithm
        ///- M. Gasser&apos;s random password generator
        ///- FIPS Publication 181.
        /// </summary>
        public static string Credits {
            get {
                return ResourceManager.GetString("Credits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This program is licensed under GNU General Public License v3.0. It comes with ABSOLUTELY NO WARRANTY. This is free software, and you are welcome to redistribute it under certain conditions..
        /// </summary>
        public static string License {
            get {
                return ResourceManager.GetString("License", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to For more information about the program and to obtain the sources, visit:.
        /// </summary>
        public static string WebSite {
            get {
                return ResourceManager.GetString("WebSite", resourceCulture);
            }
        }
    }
}
