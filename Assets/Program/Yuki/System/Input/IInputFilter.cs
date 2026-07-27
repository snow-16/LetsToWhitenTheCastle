/// <summary>
/// 入力処理をふるい分けする条件を追加するインターフェイス
/// </summary>
public interface IInputFilter
{
    /// <summary>
    /// 条件に応じて入力できるかを返すメソッド
    /// </summary>
    /// <returns>入力可能かどうか</returns>
    bool IsCanInput();
}
