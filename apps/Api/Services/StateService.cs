using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class StateService(IUnitOfWork uow)
{
  public async Task<IEnumerable<StateResponseModel>> GetAllAsync()
  {
    var states = await uow.States.GetAllAsync();
    return states.Select(ToResponse);
  }

  public async Task<StateResponseModel?> GetByIdAsync(string id)
  {
    var state = await uow.States.GetByEmailAsync(id);
    return state is null ? null : ToResponse(state);
  }

  public async Task<StateResponseModel> CreateAsync(StateCreateModel model)
  {
    var state = new State
    {
      Name = model.Name,
      Code = model.Code
    };
    var createdState = await uow.States.AddAsync(state);
    return ToResponse(createdState);
  }

  public async Task<StateResponseModel?> UpdateAsync(string id, StateUpdateModel model)
  {
    var state = await uow.States.GetByEmailAsync(id);
    if (state is null)
    {
      return null;
    }
    state.Name = model.Name;
    state.Code = model.Code;
    var updatedState = await uow.States.UpdateAsync(state);
    return ToResponse(updatedState);
  }

  public async Task<bool> DeleteAsync(string id)
  {
    if (!await uow.States.ExistsAsync(id))
    {
      return false;
    }
    await uow.States.DeleteAsync(id);
    return true;
  }

  private static StateResponseModel ToResponse(State state)
  {
    return new StateResponseModel(state.Id, state.Name, state.Code);
  }
}
