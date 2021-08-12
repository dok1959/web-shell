import React, { Component } from 'react';

export default class App extends Component {
  constructor(props) {
    super(props);
    this.handleArrowKeyDown = this.handleArrowKeyDown.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleEnterKeyPress = this.handleEnterKeyPress.bind(this);
    this.state = {
      cursorPosition : 0,
      inputCommand : '',
      commands : [],
      messagesBuffer : []
    };
    this.getAllCommands();
  }

  async getAllCommands() {
    const requestOptions = {
      method : 'GET',
      headers : { 'Content-Type' : 'application/json' }
    };
    const response = await fetch('http://localhost:55496/api/instructions', requestOptions);
    const data = await response.json();

    var inputCommand = this.state.inputCommand;
    var cursorPosition = 0;
    if(data.length > 0){
      cursorPosition = data.length - 1;
      inputCommand = data[cursorPosition];
    }

    this.setState({
      commands : data,
      inputCommand : inputCommand,
      cursorPosition : cursorPosition
    });
  }

  async postCommand(command) {
    const buffer = this.state.messagesBuffer;
    const commands = this.state.commands;
    const requestOptions = {
      method : 'POST',
      headers : { 'Content-Type' : 'application/json' },
      body : JSON.stringify({ 
        content : command
      })};
    const response = await fetch('http://localhost:55496/api/instructions', requestOptions);
    const data = await response.json();

    buffer.push(data.message);
    commands.push(command);

    this.setState({
      messagesBuffer : buffer,
      commands : commands,
      cursorPosition : commands.length - 1
    });
  }

  handleArrowKeyDown(event) {
    var result = this.state.cursorPosition;
    if(event.keyCode === 38) {
      if(result - 1 >= 0) {
        result = result - 1;
      }
      this.setState({
        cursorPosition : result,
        inputCommand : this.state.commands[result]
      });
    }
    else if (event.keyCode === 40) {
      if(result + 1 < this.state.commands.length) {
        result = result + 1;
      }
      this.setState({
        cursorPosition : result,
        inputCommand : this.state.commands[result]
      });
    }
  }

  handleSubmit = () => {
    if(this.state.inputCommand != '') {
      this.postCommand(this.state.inputCommand);
      this.setState({
        inputCommand : ''
      });
    }
  }

  handleEnterKeyPress = (event) => {
    if(event.keyCode === 13) {
      this.handleSubmit();
    }
  }
  
  handleChange = (event) => {  
    this.setState({
      inputCommand: event.target.value});
  }

  componentDidMount() {
    document.addEventListener('keydown', this.handleArrowKeyDown);
  }

  render () {
    const { messagesBuffer } = this.state;
    return (
        <div>
          <div className="input-group mb-3">
              <input className="form-control" type = "text" value = {this.state.inputCommand} onChange = {this.handleChange} onKeyDown = {this.handleEnterKeyPress}/>         
              <div class="input-group-append">
                <button class="btn btn-outline-secondary" type="button" onClick = {this.handleSubmit}>Send</button>
              </div>
          </div>
          <div className="container">
          <ul class="list-group list-group-flush">
            {
              messagesBuffer.map((value) => {
                return <li class="list-group-item">{value}</li>})
            }
          </ul>
          </div>
        </div>
        )
  }
}
