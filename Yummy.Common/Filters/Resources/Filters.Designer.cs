﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.586
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yummy.Common.Filters.Resources {
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
    internal class Filters {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Filters() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Yummy.Common.Filters.Resources.Filters", typeof(Filters).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;div id=&quot;fb-root&quot;&gt;&lt;/div&gt;
        ///&lt;form action=&quot;{{url}}&quot; method=&quot;post&quot; id=&quot;form-authorise&quot;&gt;
        ///    &lt;input type=&quot;hidden&quot; id=&quot;signed_request&quot; name=&quot;signed_request&quot; /&gt;
        ///    &lt;input type=&quot;hidden&quot; id=&quot;status&quot; name=&quot;status&quot; /&gt;
        ///    &lt;input type=&quot;hidden&quot; id=&quot;app_data&quot; name=&quot;app_data&quot; value=&quot;{{app_data}}&quot; /&gt;
        ///&lt;/form&gt;
        ///&lt;script type=&quot;text/javascript&quot;&gt;
        ///    window.fbAsyncInit = function () {
        ///        FB.init({ appId: &apos;{{appId}}&apos;, status: true, cookie: true,  xfbml: false });
        ///
        ///        if (top === self) { document.getElementById( [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string FacebookAuthorise {
            get {
                return ResourceManager.GetString("FacebookAuthorise", resourceCulture);
            }
        }
    }
}