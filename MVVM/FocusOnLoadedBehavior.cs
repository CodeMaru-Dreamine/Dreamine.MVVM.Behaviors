using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dreamine.MVVM.Behaviors.Core.Base;

namespace Dreamine.MVVM.Behaviors.MVVM
{
	/// <summary>
	/// \if KO
	/// <para>📌 컨트롤이 로드되면 자동으로 포커스를 설정하는 Behavior입니다. 주로 로그인 화면, 검색창, 입력 폼 등에서 첫 포커스 입력 요소에 사용되며, MVVM 구조를 해치지 않고 View에서 손쉽게 설정 가능합니다.</para>
	/// \endif
	/// \if EN
	/// <para>Encapsulates focus-on-load behavior functionality and related state.</para>
	/// \endif
	/// </summary>
	public class FocusOnLoadedBehavior : Behavior<FrameworkElement>
	{
		/// <summary>
		/// \if KO
		/// <para>포커스 활성화 여부를 나타내는 의존 속성입니다.</para>
		/// \endif
		/// \if EN
		/// <para>Stores the is enabled property value.</para>
		/// \endif
		/// </summary>
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.RegisterAttached(
				"IsEnabled",
				typeof(bool),
				typeof(FocusOnLoadedBehavior),
				new PropertyMetadata(false, OnIsEnabledChanged));

		/// <summary>
		/// \if KO
		/// <para>Behavior의 속성에서 포커스 활성화 여부를 가져옵니다.</para>
		/// \endif
		/// \if EN
		/// <para>Gets the is enabled value.</para>
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
		/// <para>Get Is Enabled 조건이 충족되면 <see langword="true"/>이고, 그렇지 않으면 <see langword="false"/>입니다.</para>
		/// \endif
		/// \if EN
		/// <para><see langword="true"/> when the get is enabled condition is satisfied; otherwise, <see langword="false"/>.</para>
		/// \endif
		/// </returns>
		public static bool GetIsEnabled(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsEnabledProperty);
		}

		/// <summary>
		/// \if KO
		/// <para>Behavior의 속성에서 포커스 활성화 여부를 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Sets the is enabled value.</para>
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
		public static void SetIsEnabled(DependencyObject obj, bool value)
		{
			obj.SetValue(IsEnabledProperty, value);
		}

		/// <summary>
		/// \if KO
		/// <para>IsEnabled 값이 변경되었을 때의 처리입니다. 로드 완료 시점에 포커스를 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles the is enabled changed event or state change.</para>
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
		private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is FrameworkElement element && (bool)e.NewValue)
			{
				// Loaded 이벤트가 중복 연결되지 않도록 방지
				element.Loaded -= OnElementLoaded;
				element.Loaded += OnElementLoaded;
			}
		}

		/// <summary>
		/// \if KO
		/// <para>컨트롤이 로드되면 포커스를 자동으로 설정합니다.</para>
		/// \endif
		/// \if EN
		/// <para>Handles the element loaded event or state change.</para>
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
		private static void OnElementLoaded(object sender, RoutedEventArgs e)
		{
			if (sender is FrameworkElement element)
			{
				element.Loaded -= OnElementLoaded;

				// 포커스를 이동할 수 있는 경우에만 처리
				if (element.Focusable && element.IsEnabled && element.Visibility == Visibility.Visible)
				{
					element.Focus();
					Keyboard.Focus(element);
				}
			}
		}

	}
}
