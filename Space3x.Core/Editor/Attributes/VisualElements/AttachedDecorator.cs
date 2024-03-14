using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Space3x.Core.Editor.Attributes.VisualElements
{
    public interface IEditorDecorator { }
    
    public interface IElementBlock { }
    
    [UxmlElement]
    public partial class DetachedDecorators : VisualElement
    {
        public VisualElement Origin { get; set; }
        public PropertyField RelatedField { get; set; }
        public DetachedDecorators() => AddToClassList($"ui3x-detached-decorators");
    }

    [UxmlElement]
    public partial class AutoDecorator : VisualElement, IEditorDecorator
    {
        public AutoDecorator() => AddToClassList($"ui3x-auto-decorator");
    }

    [UxmlElement]
    public partial class ButtonDecorator : AutoDecorator, IElementBlock
    {
        public ButtonDecorator() => AddToClassList($"ui3x-button-decorator");
    }
    
    
    [UxmlElement]
    public partial class VisualElementReference : VisualElement
    {
        public VisualElement Reference { get; set; }

        public override VisualElement contentContainer => Reference ?? this;
        
        public VisualElementReference() => AddToClassList($"ui3x-reference");

        public new void Add(VisualElement child) => Reference.Add(child);
    }
}
