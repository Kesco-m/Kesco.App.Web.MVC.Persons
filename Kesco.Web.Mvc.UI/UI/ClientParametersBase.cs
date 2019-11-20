using BLToolkit.Mapping;
using BLToolkit.TypeBuilder;

namespace Kesco.Web.Mvc.UI
{
    /// <summary>
    /// ������� ����� ��� ���������� �� ������� �������
    /// </summary>
    public abstract class ClientParametersBase
    {
        /// <summary>
        /// ������ ����������� ���� �� ���������
        /// </summary>
        [MapField("Width"), Parameter(600)]
        public int Width { get; set; }

        /// <summary>
        /// ������ ����������� ���� �� ���������
        /// </summary>
        [MapField("Height"), Parameter(600)]
        public int Height { get; set; }

        /*/// <summary>
        /// ������ ����������� ���� ������ ��������
        /// </summary>
        [MapField("CallDlgWidth"), Parameter(550)]
        public int CallDlgWidth { get; set; }

        /// <summary>
        /// ������ ����������� ���� ������ ��������
        /// </summary>
        [MapField("CallDlgHeight"), Parameter(230)]
        public int CallDlgHeight { get; set; }*/
    }
}