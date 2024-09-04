using DonSang.context.Models;
using System.Collections.ObjectModel;
using System.Linq;

public class DonneurQuestionnairesViewModel : BaseViewModel
{
    private readonly DonSangYJContext _dbContext;
    private readonly Donneur _selectedDonneur;

    public ObservableCollection<Questionnaire> Questionnaires { get; set; }

    public DonneurQuestionnairesViewModel(DonSangYJContext dbContext, Donneur selectedDonneur)
    {
        _dbContext = dbContext;
        _selectedDonneur = selectedDonneur;

        LoadQuestionnaires();
    }

    private void LoadQuestionnaires()
    {
        var questionnaires = _dbContext.Questionnaires
                                       .Where(q => q.IdDonneur == _selectedDonneur.IdDonneur)
                                       .ToList();

        Questionnaires = new ObservableCollection<Questionnaire>(questionnaires);
    }
}
