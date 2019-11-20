using BLToolkit.Mapping;
using BLToolkit.TypeBuilder;

namespace Kesco.Web.Mvc.UI
{
    /// <summary>
    /// Базовый класс для параметров на стороне клиента
    /// </summary>
    public abstract class ClientParametersBase
    {
        /// <summary>
        /// Ширина диалогового окна со страницей
        /// </summary>
        [MapField("Width"), Parameter(600)]
        public int Width { get; set; }

        /// <summary>
        /// Высота диалогового окна со страницей
        /// </summary>
        [MapField("Height"), Parameter(600)]
        public int Height { get; set; }

        /*/// <summary>
        /// Ширина диалогового окна звонка абоненту
        /// </summary>
        [MapField("CallDlgWidth"), Parameter(550)]
        public int CallDlgWidth { get; set; }

        /// <summary>
        /// Высота диалогового окна звонка абоненту
        /// </summary>
        [MapField("CallDlgHeight"), Parameter(230)]
        public int CallDlgHeight { get; set; }*/
    }
}