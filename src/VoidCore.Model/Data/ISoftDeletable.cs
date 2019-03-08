using System;

namespace VoidCore.Model.Data
{
    /// <summary>
    /// An entity that can be "soft-deleted." The entity is marked as deleted rather than fully deleted from the
    /// persistence. Use with caution. Soft-delete can have complications: All queries will need to filter deleted items.
    /// Performance issues caused by data buildup may need partitioning or offloading data into another table. Row
    /// uniqueness constraints will need to include DeletedOn.
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// A string representing the remover of the entity
        /// </summary>
        string DeletedBy { get; set; }

        /// <summary>
        /// The date and time the entity was deleted
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
}
