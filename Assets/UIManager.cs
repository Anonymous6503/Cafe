using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public TMP_Text coinAmountText;

    public Button addCashierBtn;
    public Button addMachineBtn;

    private void Start()
    {
        addCashierBtn.onClick.AddListener(CafeManager.Instance.cashierManager.HireNewCashier);
        addMachineBtn.onClick.AddListener(() => CafeManager.Instance.machineManager.SpawnNewMachine(MachineType.Coffee));
    }

    private void OnEnable()
    {
        PlayerWallet.OnPlayerWalletUpdate += ManageCoin;
    }

    private void OnDisable()
    {
        PlayerWallet.OnPlayerWalletUpdate -= ManageCoin;
    }

    public void ManageCoin()
    {
        coinAmountText.text = CafeManager.Instance.playerWallet.CurrentMoney.ToString("00");
    }

    public void ManageAddCashierButton(bool status)
    {
        addCashierBtn.interactable = status;
    }

    public void ManageAddMachineButton(bool status)
    {
        addMachineBtn.interactable = status;
    }
}
