﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TwitterStatisticApp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TwitterStatisticApp.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Erro ao carregar os idiomas suportados pelo Twitter..
        /// </summary>
        internal static string Carregar_ErroGetLanguages {
            get {
                return ResourceManager.GetString("Carregar_ErroGetLanguages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao carregar os tweets do Twitter..
        /// </summary>
        internal static string Carregar_ErroGetTweets {
            get {
                return ResourceManager.GetString("Carregar_ErroGetTweets", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao carregar e analisar os dados do Twitter..
        /// </summary>
        internal static string Carregar_ErroInterno {
            get {
                return ResourceManager.GetString("Carregar_ErroInterno", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao retornar os usuários com mais seguidores no Twitter..
        /// </summary>
        internal static string ObterMaisSeguidores_ErroInterno {
            get {
                return ResourceManager.GetString("ObterMaisSeguidores_ErroInterno", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao obter a lista de usuários com mais seguidores.
        /// </summary>
        internal static string ObterMaisSeguidores_ErroObter {
            get {
                return ResourceManager.GetString("ObterMaisSeguidores_ErroObter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao retornar estatística de tweets por hora..
        /// </summary>
        internal static string ObterTweetsPorHora_ErroInterno {
            get {
                return ResourceManager.GetString("ObterTweetsPorHora_ErroInterno", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao obter a estatística de tweets por hora..
        /// </summary>
        internal static string ObterTweetsPorHora_ErroObter {
            get {
                return ResourceManager.GetString("ObterTweetsPorHora_ErroObter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao retornar estatistica de tweets por tag, idioma e localidade..
        /// </summary>
        internal static string ObterTweetsPorTag_ErroInterno {
            get {
                return ResourceManager.GetString("ObterTweetsPorTag_ErroInterno", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao obter a estatistica de tweets por tag, idioma e localidade..
        /// </summary>
        internal static string ObterTweetsPorTag_ErroObter {
            get {
                return ResourceManager.GetString("ObterTweetsPorTag_ErroObter", resourceCulture);
            }
        }
    }
}
