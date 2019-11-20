using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLToolkit.Reflection;
using Kesco.ObjectModel;
using Kesco.Web.Mvc.UI.Grid;

namespace Kesco.Web.Mvc
{

	public abstract class KescoGridModelBase<T> : DialogResultModel
		where T : class
	{
		public JQGrid Grid { get; protected set; }
		public JQGridColumn EditActionIconsColumn { get; protected set; }
		public ModelMetadata Metadata { get; protected set; }


		public KescoGridModelBase(bool createEditActionIconsColumn, string returnUri, string key, string value)
			: base(returnUri, key, value)
		{
			EditActionIconsColumn = null;
			T model = TypeAccessor.CreateInstanceEx<T>();
			Metadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
			Grid = new JQGrid { 
				Columns = CreateGridColumnDefs(),
				ColumnReordering = true
			};
			SetUpGrid();
			if (createEditActionIconsColumn) CreateEditActionIconsColumn();
		}

		public KescoGridModelBase(bool createEditActionIconsColumn) : this(createEditActionIconsColumn, null, null, null) { }

		public KescoGridModelBase() : this(false, null, null, null) { }

		protected virtual void SetUpGrid()
		{
			Grid.ToolBarSettings.ShowRefreshButton = true;
			Grid.SearchDialogSettings.MultipleSearch = true;
			Grid.SearchDialogSettings.Resizable = true;
			Grid.SearchDialogSettings.ValidateInput = true;

			Grid.AppearanceSettings.AlternateRowBackground = true;
			Grid.AppearanceSettings.AlternateRowCssClass = "alternative-row";
		}

		protected virtual List<JQGridColumn> CreateGridColumnDefs()
		{
			//object model = TypeAccessor.CreateInstance();
			List<JQGridColumn> columns = new List<JQGridColumn>();

			foreach (ModelMetadata property in Metadata.Properties) {
				JQGridColumn column = CreateGridColumn(property);
				columns.Add(column);
			}

			if (Metadata.Model is ITrackableEntity) {
				JQGridColumn col = null;
				col = columns.Find((c) => {
					return c.DataField == "ChangedBy";
				});
				col.Visible = false;
				col.Editable = false;

				col = columns.Find((c) => {
					return c.DataField == "ChangedDate";
				});
				col.Visible = false;
				col.Editable = false;
			}

			return columns;
		}

		protected virtual JQGridColumn CreateGridColumn(ModelMetadata metadata)
		{
			Guard.IsNotNull(metadata, "column");
			Type type = metadata.ModelType.GetObjectTypeIfNullable();
			string modelType = type.Name;
			JQGridColumn column = new JQGridColumn();
			//column.HeaderText = metadata.DisplayName;
			column.HeaderText = String.IsNullOrEmpty(metadata.ShortDisplayName) ? metadata.GetDisplayName() : metadata.ShortDisplayName;
			//column.HeaderText += ":" + modelType;
			column.DataField = metadata.PropertyName;
			column.DataType = metadata.ModelType;
			column.Searchable = column.Visible = metadata.ShowForDisplay || metadata.ShowForEdit;
			//column.Searchable = true;
			column.Editable = !metadata.IsReadOnly;
			//column.Editable = !metadata.IsReadOnly && metadata.ShowForEdit;
			if (modelType == "Int32"
				|| modelType == "Byte"
				|| modelType == "Float" 
				|| modelType == "Decimal"
				|| modelType == "Double") {
				column.TextAlign = TextAlign.Right;
				column.SearchSupportedOperations = SearchOperations.ForNumerics;
				column.SearchToolBarOperation = SearchOperation.IsEqualTo;
				column.Width = 70;
			}
			if (modelType == "Int32" || modelType == "Byte") {
				column.Formatter = new IntegerFormatter();
			}
			if (modelType == "Float" || modelType == "Decimal") {
				column.Formatter = new NumberFormatter { 
					DecimalPlaces = 2
				};
			}
			if (modelType == "Double") {
				column.Formatter = new NumberFormatter {
					DecimalPlaces = 4
				};
			}
			if (modelType == "String") {
				//column.TextAlign = TextAlign.Center;
				//column.Formatter = new CheckBoxFormatter();
				column.SearchSupportedOperations = SearchOperations.ForStrings;
			}
			if (modelType == "Boolean") {
				column.TextAlign = TextAlign.Center;
				column.Formatter = new CheckBoxFormatter();
				column.SearchSupportedOperations = SearchOperations.IsEqualTo;
				column.EditType = EditType.CheckBox;
			}
			return column;
		}

		protected virtual void CreateEditActionIconsColumn()
		{
			Grid.Columns.Insert(0, EditActionIconsColumn = new JQGridColumn {
				Fixed = true,
				EditActionIconsColumn = true,
				EditActionIconsSettings = new EditActionIconsSettings {
					ShowEditIcon = true,
					ShowDeleteIcon = true
				},
				HeaderText = Kesco.Web.Mvc.UI.Controls.Localization.Resources.KescoGridActionColumn_Title,
				Width = 60,
				Sortable = false,
				Searchable = false
			});
		}
	}
}