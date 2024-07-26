using HomeWork.Context;
using HomeWork.Models;
using HomeWork.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberDbContext _context;
        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }
        public void AddMember(MemberViewModel member)
        {
            var newMember = new MemberModel()
            {
                MemberName = member.MemberName,
                Email = member.Email,
                Gender = member.Gender,
                Phone = member.Phone,
                MemberPassword = member.MemberPassword,
                YearOfBirth = member.YearOfBirth
            };
            _context.Add(newMember);
            _context.SaveChanges();
        }

        public void DeleteMember(int Id)
        {
            MemberModel member = _context.memberModels.Find(Id);
            if (member != null)
            {
                _context.memberModels.Remove(member);
                _context.SaveChanges();
            }
        }

        public MemberModel FindMember(string memberName, string memberPassword)
        {
            MemberModel member = _context.memberModels.FirstOrDefault(
                m => memberName.Equals(m.MemberName) &&
                memberPassword.Equals(m.MemberPassword)
            );
            return member;
        }

        public async Task<IEnumerable<MemberModel>> GetAllMember()
        {
            return await _context.memberModels.ToListAsync();
        }

        public async Task<MemberModel> GetMemberById(int Id)
        {
            return await _context.memberModels.FindAsync(Id);
        }

        public async Task<IEnumerable<MemberModel>> SearchMembersByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllMember();
            }

            return _context.memberModels.Where(m => m.MemberName.Contains(name)).ToList();
        }

        public UpdateMemberModel UpdateMember(UpdateMemberModel updateMember)
        {
            MemberModel member = _context.memberModels.Find(updateMember.id);
            member.MemberName = updateMember.MemberName;
            member.YearOfBirth = updateMember.YearOfBirth;
            member.Phone = updateMember.Phone;
            member.Gender = updateMember.Gender;
            _context.Update(member);
            _context.SaveChanges();
            return updateMember;
        }
    }
}
