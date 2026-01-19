namespace Entities.DBEntities.Base
{
    public abstract class BaseEntity<T>
    {
        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public required T Id { get; set; }
        /// <summary>
        /// Record creation date and time in UTC format
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }
        /// <summary>
        /// Record modification date and time in UTC format
        /// </summary>
        public DateTimeOffset? ModifiedOn { get; set; }
        /// <summary>
        /// Record created by
        /// </summary>
        public required string CreatedBy { get; set; }
        /// <summary>
        /// Record Modified By
        /// </summary>
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;



        #endregion
    }
}
