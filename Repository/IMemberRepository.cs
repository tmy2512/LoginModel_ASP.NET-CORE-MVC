using HomeWork.Models;
using HomeWork.Models.ViewModels;

namespace HomeWork.Repository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<MemberModel>> GetAllMember();
        void AddMember(MemberViewModel member);
        void DeleteMember(int Id);
        MemberModel FindMember(String memberName, string memberPassword);
        UpdateMemberModel UpdateMember(UpdateMemberModel member);
        Task<MemberModel> GetMemberById(int Id);
        Task<IEnumerable<MemberModel>> SearchMembersByName(string name);


    }
}
