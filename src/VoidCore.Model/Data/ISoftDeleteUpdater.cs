namespace VoidCore.Model.Data
{
    /// <summary>
    /// A service to soft delete a peristed entity. Soft deleting is marking that entity as deleted when
    /// it still exists in the persisted store.
    /// </summary>
    public interface ISoftDeleteUpdater
    {
        /// <summary>
        /// Update the soft deleted fields to show this entity as deleted.
        /// </summary>
        /// <param name="softDeletableEntity">The entity to be deleted</param>
        void Delete(ISoftDeletable softDeletableEntity);

        /// <summary>
        /// Update the soft deleted fields to show this entity as not deleted.
        /// </summary>
        /// <param name="softDeletableEntity">The entity to be un-deleted</param>
        void UnDelete(ISoftDeletable softDeletableEntity);
    }
}
