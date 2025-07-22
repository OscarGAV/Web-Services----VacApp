using Microsoft.EntityFrameworkCore.Diagnostics;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Model.Commands;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Repositories;
using VacApp_Bovinova_Platform.CampaignManagement.Domain.Services;
using VacApp_Bovinova_Platform.Shared.Domain.Repositories;

namespace VacApp_Bovinova_Platform.CampaignManagement.Application.Internal.CommandServices;

public class CampaignCommandService(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork)
:ICampaignCommandService
{
    public async Task<Campaign?> Handle(CreateCampaignCommand command)
    {
        var campaign = await campaignRepository.FindByNameAsync(command.Name);
        if(campaign!=null) throw new Exception("Campaign already exists");
        campaign = new Campaign(command);
        try
        {
            await campaignRepository.AddAsync(campaign);
            await unitOfWork.CompleteAsync();
       
        }
        catch (Exception e)
        {
           return null;
        }
        return campaign;
    }

    public async Task<IEnumerable<Campaign>> Handle(DeleteCampaignCommand command)
    {
        var campaign = await campaignRepository.FindByIdAsync(command.id);
        campaignRepository.Remove(campaign);
        var campaigns = await campaignRepository.ListAsync();
        try
        {
            await unitOfWork.CompleteAsync();
            return campaigns;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<Campaign?> Handle(UpdateCampaignStatusCommand command)
    {
        var campaign = await campaignRepository.FindByIdAsync(command.id);
        campaign.UpdateStatus(command.status);
        try
        {
            await unitOfWork.CompleteAsync();
            return campaign;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Campaign?> Handle(AddGoalToCampaignCommand command)
    {
        var campaign = await campaignRepository.FindByIdAsync(command.CampaignId);
        //var goal = campaign.Goal;
        //Console.WriteLine(goal);
        //var campaign = await campaignRepository.FindByCampaignIdAndGoalId(command.CampaignId, command.Goal.Id);
        if (campaign != null)
        {
            campaign.AddGoal(command.Goal);
            await campaignRepository.SaveChangesAsync();
        }
       
        try
        {
            await unitOfWork.CompleteAsync();
            return campaign;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Campaign?> Handle(AddChannelToCampaignCommand command)
    {
        var campaign = await campaignRepository.FindByIdAsync(command.CampaignId);
        campaign.AddChannel(command.Channel);
        try
        {
            await unitOfWork.CompleteAsync();
            return campaign;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Goal?> Handle(UpdateGoalCommand command)
    {
        throw new NotImplementedException();
    }
}