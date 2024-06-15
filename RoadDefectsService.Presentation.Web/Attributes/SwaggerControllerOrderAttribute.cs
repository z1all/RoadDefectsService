namespace RoadDefectsService.Presentation.Web.Attributes
{
    public class SwaggerControllerOrderAttribute : Attribute
    {
        public required int Order { get;  set; }
    }
}
