using StudySpaceApp.DAO;
using StudySpaceApp.DTO;
using StudySpaceApp.Models;

namespace StudySpaceApp.Service
{
    public class NoteServiceImpl : INoteService
    {
        private readonly INoteDAO _noteDAO;

        public NoteServiceImpl(INoteDAO noteDAO)
        {
            _noteDAO = noteDAO;
        }

        public NoteReadOnlyDTO? Insert(NoteInsertDTO insertDTO)
        {
            Note note = new Note
            {
                Content = insertDTO.Content,
                CreatedAt = DateTime.Now,
                UserId = insertDTO.UserId
            };

            Note? insertedNote =
                _noteDAO.Insert(note);

            if (insertedNote == null)
            {
                return null;
            }

            return new NoteReadOnlyDTO
            {
                Id = insertedNote.Id,
                Content = insertedNote.Content,
                CreatedAt = insertedNote.CreatedAt,
                UserId = insertedNote.UserId
            };
        }

        public List<NoteReadOnlyDTO> GetAllByUserId(int userId)
        {
            List<Note> notes =
                _noteDAO.GetAllByUserId(userId);

            List<NoteReadOnlyDTO> noteDTOs =
                new List<NoteReadOnlyDTO>();

            foreach (Note note in notes)
            {
                NoteReadOnlyDTO noteDTO =
                    new NoteReadOnlyDTO
                    {
                        Id = note.Id,
                        Content = note.Content,
                        CreatedAt = note.CreatedAt,
                        UserId = note.UserId
                    };

                noteDTOs.Add(noteDTO);
            }

            return noteDTOs;
        }

        public bool Delete(int id, int userId)
        {
            return _noteDAO.Delete(id, userId);
        }
    }
}