//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace oooRuli
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PointId { get; set; }
        public int UserId { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Point Point { get; set; }
        public virtual User User { get; set; }
    }
}