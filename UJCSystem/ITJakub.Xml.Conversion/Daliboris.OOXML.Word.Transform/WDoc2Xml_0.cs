﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:2.0.50727.4200
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace Daliboris.OOXML.Word.Transform {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class transformace {
        
        private koren korenField;
        
        private tag[] tagyField;
        
        private citac[] citaceField;
        
        private nahrada[] nahradyField;
        
        private tabulky tabulkyField;
        
        private System.DateTime posledniZmenaField;
        
        /// <remarks/>
        public koren koren {
            get {
                return this.korenField;
            }
            set {
                this.korenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("tag", IsNullable=false)]
        public tag[] tagy {
            get {
                return this.tagyField;
            }
            set {
                this.tagyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("citac", IsNullable=false)]
        public citac[] citace {
            get {
                return this.citaceField;
            }
            set {
                this.citaceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("nahrada", IsNullable=false)]
        public nahrada[] nahrady {
            get {
                return this.nahradyField;
            }
            set {
                this.nahradyField = value;
            }
        }
        
        /// <remarks/>
        public tabulky tabulky {
            get {
                return this.tabulkyField;
            }
            set {
                this.tabulkyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime posledniZmena {
            get {
                return this.posledniZmenaField;
            }
            set {
                this.posledniZmenaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class koren {
        
        private atribut[] atributField;
        
        private string namespaceField;
        
        private string nazevField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("atribut")]
        public atribut[] atribut {
            get {
                return this.atributField;
            }
            set {
                this.atributField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @namespace {
            get {
                return this.namespaceField;
            }
            set {
                this.namespaceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
        public string nazev {
            get {
                return this.nazevField;
            }
            set {
                this.nazevField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class atribut {
        
        private string hodnotaField;
        
        private string nazevField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string hodnota {
            get {
                return this.hodnotaField;
            }
            set {
                this.hodnotaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
        public string nazev {
            get {
                return this.nazevField;
            }
            set {
                this.nazevField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class tag {
        
        private atribut[] atributField;
        
        private nahrada[] nahradaField;
        
        private bool bezZnackyField;
        
        private bool bezZnackyFieldSpecified;
        
        private bool ignorovatField;
        
        private bool ignorovatFieldSpecified;
        
        private string namespaceField;
        
        private string nazevField;
        
        private bool prazdnyElementField;
        
        private bool prazdnyElementFieldSpecified;
        
        private string predchoziStylField;
        
        private bool sloucitSPredchazejicimField;
        
        private bool sloucitSPredchazejicimFieldSpecified;
        
        private string nasledujiciStylField;
        
        private bool sloucitSNasledujicimField;
        
        private bool sloucitSNasledujicimFieldSpecified;
        
        private string stylField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("atribut")]
        public atribut[] atribut {
            get {
                return this.atributField;
            }
            set {
                this.atributField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nahrada")]
        public nahrada[] nahrada {
            get {
                return this.nahradaField;
            }
            set {
                this.nahradaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool bezZnacky {
            get {
                return this.bezZnackyField;
            }
            set {
                this.bezZnackyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool bezZnackySpecified {
            get {
                return this.bezZnackyFieldSpecified;
            }
            set {
                this.bezZnackyFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ignorovat {
            get {
                return this.ignorovatField;
            }
            set {
                this.ignorovatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ignorovatSpecified {
            get {
                return this.ignorovatFieldSpecified;
            }
            set {
                this.ignorovatFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @namespace {
            get {
                return this.namespaceField;
            }
            set {
                this.namespaceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
        public string nazev {
            get {
                return this.nazevField;
            }
            set {
                this.nazevField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool prazdnyElement {
            get {
                return this.prazdnyElementField;
            }
            set {
                this.prazdnyElementField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool prazdnyElementSpecified {
            get {
                return this.prazdnyElementFieldSpecified;
            }
            set {
                this.prazdnyElementFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string predchoziStyl {
            get {
                return this.predchoziStylField;
            }
            set {
                this.predchoziStylField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool sloucitSPredchazejicim {
            get {
                return this.sloucitSPredchazejicimField;
            }
            set {
                this.sloucitSPredchazejicimField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sloucitSPredchazejicimSpecified {
            get {
                return this.sloucitSPredchazejicimFieldSpecified;
            }
            set {
                this.sloucitSPredchazejicimFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nasledujiciStyl {
            get {
                return this.nasledujiciStylField;
            }
            set {
                this.nasledujiciStylField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool sloucitSNasledujicim {
            get {
                return this.sloucitSNasledujicimField;
            }
            set {
                this.sloucitSNasledujicimField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sloucitSNasledujicimSpecified {
            get {
                return this.sloucitSNasledujicimFieldSpecified;
            }
            set {
                this.sloucitSNasledujicimFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string styl {
            get {
                return this.stylField;
            }
            set {
                this.stylField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class nahrada {
        
        private string coField;
        
        private string cimField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string co {
            get {
                return this.coField;
            }
            set {
                this.coField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cim {
            get {
                return this.cimField;
            }
            set {
                this.cimField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class citac {
        
        private string formatField;
        
        private int hodnotaField;
        
        private int inkrementField;
        
        private string inkrementatorField;
        
        private string nazevField;
        
        private string postfixField;
        
        private string prefixField;
        
        private string resetatorField;
        
        private int vychoziHodnotaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int hodnota {
            get {
                return this.hodnotaField;
            }
            set {
                this.hodnotaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int inkrement {
            get {
                return this.inkrementField;
            }
            set {
                this.inkrementField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string inkrementator {
            get {
                return this.inkrementatorField;
            }
            set {
                this.inkrementatorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="NCName")]
        public string nazev {
            get {
                return this.nazevField;
            }
            set {
                this.nazevField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string postfix {
            get {
                return this.postfixField;
            }
            set {
                this.postfixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string prefix {
            get {
                return this.prefixField;
            }
            set {
                this.prefixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string resetator {
            get {
                return this.resetatorField;
            }
            set {
                this.resetatorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int vychoziHodnota {
            get {
                return this.vychoziHodnotaField;
            }
            set {
                this.vychoziHodnotaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class tabulky {
        
        private string tabulkaField;
        
        private string radekField;
        
        private string bunkaField;
        
        private string obsahPrazdneBunkyField;
        
        private string textMistoTabulkyField;
        
        private bool cislovatElementyField;
        
        private bool cislovatElementyFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tabulka {
            get {
                return this.tabulkaField;
            }
            set {
                this.tabulkaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string radek {
            get {
                return this.radekField;
            }
            set {
                this.radekField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string bunka {
            get {
                return this.bunkaField;
            }
            set {
                this.bunkaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string obsahPrazdneBunky {
            get {
                return this.obsahPrazdneBunkyField;
            }
            set {
                this.obsahPrazdneBunkyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string textMistoTabulky {
            get {
                return this.textMistoTabulkyField;
            }
            set {
                this.textMistoTabulkyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool cislovatElementy {
            get {
                return this.cislovatElementyField;
            }
            set {
                this.cislovatElementyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cislovatElementySpecified {
            get {
                return this.cislovatElementyFieldSpecified;
            }
            set {
                this.cislovatElementyFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class tagy {
        
        private tag[] tagField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tag")]
        public tag[] tag {
            get {
                return this.tagField;
            }
            set {
                this.tagField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class citace {
        
        private citac[] citacField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("citac")]
        public citac[] citac {
            get {
                return this.citacField;
            }
            set {
                this.citacField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schema.brus.cz/2010/WDoc2Xml.xsd", IsNullable=false)]
    public partial class nahrady {
        
        private nahrada[] nahradaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nahrada")]
        public nahrada[] nahrada {
            get {
                return this.nahradaField;
            }
            set {
                this.nahradaField = value;
            }
        }
    }
}
