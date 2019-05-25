using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;
using InternShipBackend.Request;
using InternShipBackend.Response;

namespace InternShipBackend.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveAll();

        StudentResponse getStudent(int id);
        List<StudentResponse> GetStudents();
        List<RecourseResponse> GetRecourses();
        List<RecourseResponse> GetRecoursesFalse();
        List<StudentResponseForAcademician> GetStudentForHome();
        List<Teacher> getTeachers();
        List<AssignmentResponse> getAssignment(int teacherId);
        CompanyResponse getCompany(int id);
        RecourseResponse getRecourse(int id);
        List<CompanyResponse> getAllCompanies();
        bool setRecourse(int id);
        bool setRecourseReject(int id);
        DateTime[] createDays(InternShipRequest req);
        TeacherResponse getTeacher(int id);
        bool setCompanyId(int id, int companyId);
        bool assignment();
        bool editStudent(string image, int id);
        bool editDay(DayRequest day);
        bool isInternShipOK(int id);
        bool GradingSettings(int num);
        bool IsMyStudent(int studentId,int academicianId);
        List<SpecificDays> GetDates();
        DayResponseForModal GetDay(int id);
        GradingSetting GetSetting();
        bool SetGrade(GradeRequest req);
        bool isGradeExist(int id);

        GradeResponse GetGradeResponse(int id);

        List<DayResponse> getDaysByStudent(int id);
       

        FileResponse GetFileResponse(int id);
        AssignmentResponseForStudent GetAssignmentForStudent(int id);
        List<AssignmentResponse> GetAllAssignment();
        List<StudentResponseForAcademician> GetStudentsForAcademicians(int id);




    }
}
