//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace REST_Agencia_Viajes
{
    using System;
    using System.Collections.Generic;
    
    public partial class Viajeros_Viajes
    {
        public int ID_Viajeros_Viajes { get; set; }
        public int ID_FK_Viajeros { get; set; }
        public int ID_FK_Viajes_Disponibles { get; set; }
        public bool Activo { get; set; }
    
        public virtual Viajeros Viajeros { get; set; }
        public virtual Viajes_Disponibles Viajes_Disponibles { get; set; }
    }
}
