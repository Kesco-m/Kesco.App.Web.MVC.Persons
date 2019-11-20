using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kesco.ComponentModel.DataAnnotations;

namespace Kesco.Web.Mvc.UI.ComponentModel.DataAnnotations
{

    /// <summary>
    /// Атрибут, описывающий команду, которую должен поддерживать 
    /// элемент управления SelectBox
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited=true)]
    public class KescoSelectCommandAttribute : Attribute
    {

        // TODO: Добавить свойство Appearance

        private LocalizableString _commandText = new LocalizableString("CommandText");

        /// <summary>
        /// Возвращает или устанавливает идентификатор команды, 
        /// которую должен поддерживать элемент управления SELECT
        /// </summary>
        /// <value>
        /// Идентификатор команды. Например, "advSearch".
        /// </value>
        public string Command { get; set; }

        /// <summary>
        /// Возвращает или устанавливает иконку, связанную с данной командой
        /// </summary>
        /// <value>
        /// Иконку, связанная с ссылкой.
        /// </value>
        public string CommandIcon { get; set; }

        /// <summary>
        /// Возвращает или устанавливает текст команды для пользователя.  
        /// Текст может быть локализован. В этом случае данное свойство 
        /// указывает соответствущее имя свойства из ресурса, 
        /// которое содержит локализованное значение.
        /// </summary>
        /// <value>
        /// Текст команды или имя свойства, если указан тип ресурса.
        /// </value>
        public string CommandText
        {
            get { return _commandText.Value; }
            set {
                if (_commandText.Value != value)
                {
                    _commandText.Value = value;
                }
            }
        }

        /// <summary>
        /// Возвращает или устанавливает тип ресурса 
        /// для получения локализованной строки команды.  
        /// </summary>
        /// <value>
        /// Тип ресурса
        /// </value>
        public Type ResouceType
        {
            get { return _commandText.ResourceType; }
            set
            {
                if (_commandText.ResourceType != value)
                {
                    _commandText.ResourceType = value;
                }
            }
        }

        /// <summary>
        /// Иницилизирует новый экземпляр <see cref="PersonControlAttribute" /> класса.
        /// </summary>
        /// <param name="command">Идентификатор команды.</param>
        /// <param name="commandText">Текст команды или свойство ресурса.</param>
        /// <param name="commandIcon">Иконка для команды.</param>
        /// <param name="resourceType">Тип ресурса для получения локализованной версии.</param>
        public KescoSelectCommandAttribute(string command, string commandText, string commandIcon, Type resourceType)
            : base()
        {
            ResouceType = resourceType;
            Command = command;
            CommandText = commandText;
            CommandIcon = commandIcon;
        }

        /// <summary>
        /// Возвращает локализованное значение текста команды.
        /// </summary>
        /// <returns>Локализованное значение текста команды</returns>
        public string GetCommandText()
        {
            return _commandText.GetLocalizableValue();
        }

    }
}