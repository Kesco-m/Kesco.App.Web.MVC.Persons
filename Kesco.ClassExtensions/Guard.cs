namespace Kesco
{
    using System;

	/// <summary>
	/// �����, �������������� ������ ��� �������� ����������.
	/// </summary>
    internal static class Guard
    {
		/// <summary>
		/// ��������� �������� �� ������ �� �������� ������.
		/// ���������� ���������� <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">������ �� ��������.</param>
		/// <param name="parameterName">��� ���������.</param>
        public static void IsNotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, " cannot be null.");
            }
        }

		/// <summary>
		/// ��������� �������� �� ������ �� �������� ������.
		/// ���������� ���������� <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">������ �� ��������.</param>
		/// <param name="parameterName">��� ���������.</param>
		/// <param name="message">��������� �� ������.</param>
        public static void IsNotNull(object parameter, string parameterName, string message)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, " " + message);
            }
        }

		/// <summary>
		/// ��������� �������� ������ ������.
		/// ���������� ���������� <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">������.</param>
		/// <param name="parameterName">��� ���������.</param>
		public static void IsNotNullOrEmpty(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException(parameterName, " cannot be null or empty.");
            }
        }

		/// <summary>
		/// ��������� �������� �� ������ ������.
		/// ���������� ���������� <see cref="System.ArgumentNullException"/>
		/// </summary>
		/// <param name="parameter">������ �� ��������.</param>
		/// <param name="parameterName">��� ���������.</param>
		/// <param name="message">��������� �� ������.</param>
		public static void IsNotNullOrEmpty(string parameter, string parameterName, string message)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException(parameterName, " " + message);
            }
        }
    }
}
