using StudySpaceApp.DTO;

namespace StudySpaceApp.Service
{
    public interface INoteService
    {
        NoteReadOnlyDTO? Insert(NoteInsertDTO insertDTO);

        List<NoteReadOnlyDTO> GetAllByUserId(int userId);

        bool Delete(int id, int userId);
    }
}