//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopApplication.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class comments
    {
        public int commentID { get; set; }
        public string message { get; set; }
        public int masterID { get; set; }
        public int requestID { get; set; }
    
        public virtual masters masters { get; set; }
        public virtual requests requests { get; set; }
    }
}
