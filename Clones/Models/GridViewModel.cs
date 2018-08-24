using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Clones.Models
{
    public interface IGridViewModel
    {
        Type EntityType { get; set; }

        IEnumerable<object> Entities { get; set; }

        List<GridColumnViewModel> Columns { get; set; }

        string PrimaryKeyName { get; set; }

        object CreateNewInstance();
    }

    public class GridViewModel<T> : IGridViewModel
        where T : class
    {
        public GridViewModel(IEnumerable<T> entities, string primaryKeyName = "Id", List<GridColumnViewModel> columns = null)
        {
            EntityType = typeof(T);
            Entities = entities;
            PrimaryKeyName = primaryKeyName;
            Columns = columns ?? new List<GridColumnViewModel>();
        }

        public Type EntityType { get; set; }

        public IEnumerable<object> Entities { get; set; }

        public List<GridColumnViewModel> Columns { get; set; }

        public string PrimaryKeyName { get; set; }

        public object CreateNewInstance()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class GridColumnViewModel
    {
        public GridColumnViewModel(string propertyName, ControlType columnType = ControlType.Input, SelectList selectList = null)
        {
            PropertyName = propertyName;
            ColumnType = columnType;
            SelectList = selectList;
        }

        public string PropertyName { get; set; }
        public ControlType ColumnType { get; set; }
        public SelectList SelectList { get; set; }
    }

    /// <summary>
    /// Types of edit controls for editable table
    /// </summary>
    public enum ControlType
    {
        Hidden = 1,
        Input = 2,
        TextArea = 3,
        Select = 4
    }
}