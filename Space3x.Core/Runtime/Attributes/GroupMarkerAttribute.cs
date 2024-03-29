﻿using System;
using UnityEngine;

namespace Space3x.Core.Attributes
{
    public enum GroupType { None, Row, Column }
    
    [AttributeUsage(AttributeTargets.Class 
                    | AttributeTargets.Method 
                    | AttributeTargets.Property 
                    | AttributeTargets.Field, 
        AllowMultiple = true, Inherited = true)]
    public class GroupMarkerAttribute : PropertyAttribute
    {
        public string Text { get; set; }
        
        public GroupType Type { get; set; }
        
        /// <summary>
        /// Whether this attribute is for the opening marker or the closing marker.
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// Group serialized members within this attribute marker to it's corresponding closing attribute.
        /// </summary>
        /// <param name="groupType"></param>
        public GroupMarkerAttribute(GroupType groupType) => this.Type = groupType;
        
        public GroupMarkerAttribute(GroupType groupType, string text = "", bool isOpen = false)
        {
            this.Type = groupType;
            this.Text = text;
            this.IsOpen = isOpen;
        }
    }
}
