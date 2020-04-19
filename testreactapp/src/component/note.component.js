import React, { Component } from "react";
import { noteService } from '../services/note.service';
import Pagination from '../component/pager.component';


class Note extends Component {
  constructor(props){
    super(props);

    this.state = {
      timer: null,
      noteItems: [],
      pageOfItems: [],
      loaded: false,
      isEditData: false,
      isShowOperation : false,
      cbValue: 'EN'
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.getTheads = this.getTheads.bind(this);
    this.getTBoby = this.getTBoby.bind(this);
    this.onChangePage = this.onChangePage.bind(this);
    this.onSaveClick = this.onSaveClick.bind(this);
    this.onRemoveClick = this.onRemoveClick.bind(this);  
    this.hideShowOperation = this.hideShowOperation.bind(this);
    this.sleep = this.sleep.bind(this);
    this.handleChangeComboBox = this.handleChangeComboBox.bind(this);
  };

  handleChangeComboBox(e){
    var lang = e.target.value;
    const { i18n } = this.props;

    this.setState({ cbValue : lang}, () => {         
    });   

    if(lang == 'en'){
        i18n.changeLanguage('en')
    }
    else if(lang == 'rus'){
        i18n.changeLanguage('ru')
    }
  }

  hideShowOperation() {
    clearTimeout(this.state.timer);
    this.setState({ isShowOperation: false });
  }

  hideWithTimer() {
    this.state.timer = setTimeout(() => {
      this.hideShowOperation()
    }, 1500)
  }

  componentDidMount() {
     if(!this.state.loaded){        
      noteService.getAll().then(data => {       
          this.setState({ noteItems: data }, () => {         
          });
        }).catch(error => {
          console.log(error);
      });        
    }  
    
    this.setState({
      loaded: true
     }); 
  }

  sleep (time) {
    return new Promise((resolve) => setTimeout(resolve, time));
  }

  handleSubmit(e){      
    e.preventDefault();
    
    const {cbValue} = this.state;

    var noteMessage = e.target[0].value;
    var note = {     
      'noteMessage': noteMessage,    
    };

    noteService.addNote(note)

    this.sleep(100).then(() => {
        noteService.getAll().then(data => {
            this.hideWithTimer();

            this.setState({ noteItems: data, isShowOperation: true}, () => {         
            });          
        }).catch(error => {
            console.log(error);
        });
    });
  
    e.target[0].value = '';
  }

  getTheads(props){
    
    const theadNames = props.theads.map((p) => {             
        return <th key={p.propName}>{p.propName}</th>;
    }); 
    return theadNames;
  } 

  getTBoby(props){
    const { withTranslation } = this.props

    const tBodyItems = props.pageOfItems.map(note => {
      return <tr key={note.id}>
          <td align="center">{note.id }</td>
          <td align="center">
              <textarea cols="40" rows="2" id={"textarea" + note.id} style={{width: '95%'}} required
                onChange={e => { this.onChangeTextArea(e, note.id)}} defaultValue={note.noteMessage}>
              </textarea>
          </td>
          <td align="center">                          
            <button type="button" className="btn btn-secondary" style={{marginLeft: '5px'}}
              onClick={e => { this.onSaveClick(e, note.id)}}>{withTranslation('notepage.tbodyactionsave')}</button> 
            <button type="button" className="btn btn-danger" style={{marginLeft: '5px'}}
              onClick={e => { this.onRemoveClick(e, note.id)}}>{withTranslation('notepage.tbodyactiondelete')}</button>                                      
          </td> 
       </tr>;
    }); 
    return tBodyItems;
  }

  onChangePage(pageOfItems) {
    this.setState({ pageOfItems: pageOfItems });
  }

  onChangeTextArea(e, id){    
    this.setState({
      isEditData: true
    });
  }
  
  onSaveClick(e, id){ 
    const { isEditData } = this.state;
   
    if(!isEditData){      
      return;
    }

    var textArea = document.getElementById('textarea' + id);
    if(textArea == null){
      return;
    }
   
    noteService.getById(id).then(data => {       
      var note = data;
      note.noteMessage = textArea.value;
      noteService.edit(id, note);
      
      this.hideWithTimer();

        this.setState({ isShowOperation: true }, () => {             
        });      
      }).catch(error => {
        console.log(error);
    }); 

    this.sleep(100).then(() => {
        noteService.getAll().then(data => {       
            this.setState({ noteItems: data }, () => {         
            });
        }).catch(error => {
            console.log(error);
        }); 
    });

    this.setState({
        isEditData: false
    });

    this.render();
  }

  onRemoveClick(e, id){
    noteService.delete(id);

    this.sleep(100).then(() => {
        noteService.getAll().then(data => { 
            this.hideWithTimer();     

            this.setState({ noteItems: data, isShowOperation: true }, () => {             
            });        
        }).catch(error => {
            console.log(error);
        }); 
    });

    this.render();
  }

  render() {
    const { noteItems, pageOfItems, isShowOperation } = this.state;
    const { withTranslation, i18n } = this.props
    const tableTheads =[{ propName : withTranslation('notepage.theadfirst')},
                        { propName : withTranslation('notepage.theadsecond')},
                        { propName : withTranslation('notepage.theadthird')}];
  
    return (
       <div className="container">
        <br />
    
        <div className="d-flex flex-row bd-highlight mb-3">
            <div className="p-2 bd-highlight" >
                <label>{ withTranslation('notepage.labelcombobox') }</label>
            </div>
            <div className="p-2 bd-highlight" >
                <select value={this.state.value} onChange={this.handleChangeComboBox}>
                    <option value="en">EN</option>
                    <option value="rus">RUS</option>                    
                </select>  
            </div>
        </div>
    
    {noteItems != null && noteItems.length > 0 && (                    
        <table className="table table-striped">
            <thead>
                <tr><this.getTheads theads={tableTheads} /></tr>
            </thead>
            <tbody><this.getTBoby pageOfItems={pageOfItems} /></tbody>
        </table>
    )}
        <hr />
        <div className="d-flex flex-row bd-highlight mb-3">
            <div className="p-2 bd-highlight" >
                <form className="form-inline" onSubmit={this.handleSubmit}>                       
                    <div className="form-group mx-sm-3 mb-2">              
                    <input required className="form-control" type="text"/>                  
                    </div>             
                    <input type="submit" className="btn btn-primary mb-2" value={ withTranslation('notepage.sumbitbtn') } />                                     
                </form>
            </div>         
          </div>
          <div className="p-2 bd-highlight">             
                <Pagination items={noteItems}
                            onChangePage={this.onChangePage} 
                            i18n={i18n}
                            withTranslation={withTranslation}/>              
            </div>
          {isShowOperation && (
            <div className="d-flex justify-content-center">  
              <strong style={{marginLeft: '5px', color: 'green'}}>{ withTranslation('notepage.opmessage') }</strong>                   
            </div>
          )}
      </div>
    );}
}

export default Note;