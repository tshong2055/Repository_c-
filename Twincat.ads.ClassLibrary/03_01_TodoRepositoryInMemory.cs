using System.Collections.Generic;

namespace TodoApp.Models
{
    //############
    public class TodoRepositioryInMemory : ITodoRepository
    {
        private static List<Todo> _todos = new List<Todo>();

        public TodoRepositioryInMemory()
        {
            _todos = new List<Todo>
            { 
                new Todo { Id = 1, HandleName = "TechnologyLab", HandleValue = 123},            
            };
        }

        //인_메모리데이터베이스 
        //##### 입력
        public void Add(Todo model)
        {
            model.Id =_todos.Max(t =>t.Id)+1; //아이디중 가장큰것에서 +1 하기 
            _todos.Add(model);
        }

        //##### 출력 
        public List<Todo> GetAll()
        {
           return _todos.ToList();
        }

        //##### 
        public uint HanbleName()
        {
            
            return 0;
        }
    }

}
