import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../../store';
import * as PhonebookStore from './phonebookStore';

// At runtime, Redux will merge together...
type PhonebookProps =
   PhonebookStore.PhonebookState // ... state we've requested from the Redux store
   & typeof PhonebookStore.actionCreators // ... plus action creators we've requested
   & RouteComponentProps<{ startIndex: string }>; // ... plus incoming routing parameters


class Phonebook extends React.PureComponent<PhonebookProps> {
   // This method is called when the component is first added to the document
   public componentDidMount() {
      this.ensureDataFetched();
   }

   // This method is called when the route parameters change
   public componentDidUpdate() {
      this.ensureDataFetched();
   }

   private fetchEntries = () => {
      const startIndex = parseInt(this.props.match.params.startIndex, 10) || 0;
      this.props.requestPhonebookEntries(startIndex);
   }
   
   //<p>Phonebook Name: <input type="text" value={this.props.name} /> {this.props.id}</p>
   public render() {
      return (
         <React.Fragment>
            <h1 id="tabelLabel">Phonebook</h1>
            <p>Phonebook Name: {this.props.name}  {this.props.id} <button
               onClick={this.fetchEntries}>Fetch</button> </p>
            
            {this.renderPhonebook()}
            {this.renderPagination()}
         </React.Fragment>
      );
   }

   private ensureDataFetched() {
      const startIndex = parseInt(this.props.match.params.startIndex, 10) || 0;
      this.props.requestPhonebook(this.props.name);
      this.props.requestPhonebookEntries(startIndex);
   }

   private renderPhonebook() {
      return (
         <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
               <tr>
                  <th>Name</th>
                  <th>Phone Number</th>
               </tr>
            </thead>
            <tbody>
               {this.props.entries.map((entry: PhonebookStore.PhonebookEntries) =>
                  <tr key={entry.id}>
                     <td>{entry.name}</td>
                     <td>{entry.PhoneNumber}</td>
                  </tr>
               )}
            </tbody>
         </table>
      );
   }

   private renderPagination() {
      const prevStartIndex = (this.props.startIndex || 0) - 5;
      const nextStartIndex = (this.props.startIndex || 0) + 5;

      return (
         <div className="d-flex justify-content-between">
            <Link className='btn btn-outline-secondary btn-sm' to={`/phonebook/${prevStartIndex}`}>Previous</Link>
            {this.props.isLoading && <span>Loading...</span>}
            <Link className='btn btn-outline-secondary btn-sm' to={`/phonebook/${nextStartIndex}`}>Next</Link>
         </div>
      );
   }
}

export default connect(
   (state: ApplicationState) => state.phonebook, // Selects which state properties are merged into the component's props
   PhonebookStore.actionCreators // Selects which action creators are merged into the component's props
)(Phonebook as any);
