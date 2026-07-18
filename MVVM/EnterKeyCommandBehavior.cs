using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dreamine.MVVM.Behaviors.Core.Base;

namespace Dreamine.MVVM.Behaviors.MVVM
{
	/// <summary>
	/// \if KO
	/// <para>사용자가 Enter 키를 누를 때 지정된 ICommand를 실행하는 Behavior입니다. MVVM 구조에서 ViewModel의 명령(예: 로그인, 검색 실행 등) TextBox 또는 Input 계열 컨트롤에서 직접 트리거할 수 있도록 연결합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Encapsulates enter-key command behavior functionality and related state.</para>
	/// \endif
	/// </summary>
	public class EnterKeyCommandBehavior : Behavior<UIElement>
	{
		/// <summary>
		/// \if KO
		/// <para>실행할 커맨드를 지정하는 의존 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Stores the command property value.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command",
				typeof(ICommand),
				typeof(EnterKeyCommandBehavior),
				new PropertyMetadata(null, OnCommandChanged));

		/// <summary>
		/// \if KO
		/// <para>커맨드를 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the command value.</para>
		/// \endif
		/// </summary>
		/// <param name="obj">
		/// \if KO
		/// <para>동작 또는 연결 속성을 적용할 종속성 객체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object to which the behavior or attached property applies.</para>
		/// \endif
		/// </param>
		/// <returns>
		/// \if KO
		/// <para>Get Command 작업에서 생성한 <see cref="ICommand"/> 결과입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The <see cref="ICommand"/> result produced by the get command operation.</para>
		/// \endif
		/// </returns>
		public static ICommand? GetCommand(DependencyObject obj)
		{
			return (ICommand?)obj.GetValue(CommandProperty);
		}

		/// <summary>
		/// \if KO
		/// <para>커맨드를 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets the command value.</para>
		/// \endif
		/// </summary>
		/// <param name="obj">
		/// \if KO
		/// <para>동작 또는 연결 속성을 적용할 종속성 객체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object to which the behavior or attached property applies.</para>
		/// \endif
		/// </param>
		/// <param name="value">
		/// \if KO
		/// <para>적용할 값입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The value to apply.</para>
		/// \endif
		/// </param>
		public static void SetCommand(DependencyObject obj, ICommand? value)
		{
			obj.SetValue(CommandProperty, value);
		}

		/// <summary>
		/// \if KO
		/// <para>커맨드 속성이 변경되면 KeyDown 이벤트를 연결합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles the command changed event or state change.</para>
		/// \endif
		/// </summary>
		/// <param name="d">
		/// \if KO
		/// <para>동작 또는 연결 속성을 적용할 종속성 객체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The dependency object to which the behavior or attached property applies.</para>
		/// \endif
		/// </param>
		/// <param name="e">
		/// \if KO
		/// <para>이벤트와 관련된 데이터를 포함합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Contains data associated with the event.</para>
		/// \endif
		/// </param>
		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is UIElement element)
			{
				element.KeyDown -= OnKeyDown;
				element.KeyDown += OnKeyDown;
			}
		}

		/// <summary>
		/// \if KO
		/// <para>Enter 키가 눌릴 경우 Command를 실행합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles the key down event or state change.</para>
		/// \endif
		/// </summary>
		/// <param name="sender">
		/// \if KO
		/// <para>이벤트를 발생시킨 객체입니다.</para>
		/// \endif
		/// \if EN
		/// <para>The object that raised the event.</para>
		/// \endif
		/// </param>
		/// <param name="e">
		/// \if KO
		/// <para>이벤트와 관련된 데이터를 포함합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Contains data associated with the event.</para>
		/// \endif
		/// </param>
		private static void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter && sender is DependencyObject obj)
			{
				ICommand? command = GetCommand(obj);
				if (command?.CanExecute(null) == true)
				{
					command.Execute(null);
					e.Handled = true;
				}
			}
		}
	}
}
