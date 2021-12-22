
// public struct HpInfo
// {
//     public int maxhp;
//     public int curhp;
//     public HpInfo(int _curhp, int _maxhp)
//     {
//         curhp = _curhp;
//         maxhp = _maxhp;
//     }
// }

public interface IDamagable
{
    bool TakeDamage(int damageValue);
}
    