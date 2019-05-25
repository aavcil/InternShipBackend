using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;

namespace InternShipBackend.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password,int userId);
        Task<Student> StudentRegister(Student student);

        Task<User> Login(string userName, string password);

        Task<TeachersLogin> TeacherRegister(TeachersLogin user, string password, int teacherId);
        Task<Teacher> TeacherCreate(Teacher teacher);

        Task<TeachersLogin> TeacherLogin(string userName, string password);
        Task<bool> UserExists(string userName);
        Task<bool> TeacherExists(string userName);

        long studentId(int id);



    }
}
