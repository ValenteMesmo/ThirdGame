namespace Common
{
    public class MultipleInputSource : Inputs
    {
        private readonly Inputs[] inputs;

        public int Direction { get; set; }
        public int Action { get; set; }

        public MultipleInputSource(params Inputs[] inputs)
        {
            this.inputs = inputs;
        }

        public void Update()
        {
            Direction = DpadDirection.None;
            Action = DpadDirection.None;

            foreach (var item in inputs)
            {
                item.Update();

                if (item.Direction != DpadDirection.None)
                    Direction = item.Direction;

                if (item.Action != DpadDirection.None)
                    Action = item.Action;
            }
        }
    }


}
