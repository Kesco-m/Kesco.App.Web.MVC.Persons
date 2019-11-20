<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int?>" %>
<%= (Model.HasValue) ? Model.Value.ToString() : "---" %>