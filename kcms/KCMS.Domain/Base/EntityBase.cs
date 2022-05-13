using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCMS.Domain.Base
{
    public interface IEntityBase
    {
        long Id { get; set; }
    }

    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
    }

    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }
    }

    public abstract class DeleteEntity : EntityBase, IDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }

    public abstract class AuditEntity : DeleteEntity, IAuditEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
