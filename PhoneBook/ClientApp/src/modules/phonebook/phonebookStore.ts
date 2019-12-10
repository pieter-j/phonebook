import { Action, Reducer } from 'redux';
import { AppThunkAction } from '../../store';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface Phonebook {
   id: number;
   name: string;
}

export interface PhonebookState {
   isLoading: boolean;
   id: number;
   name: string;
   entries: PhonebookEntries[];
   newEntry: PhonebookEntries;
   startIndex: number;
   errors: string
}

export interface PhonebookEntries {
   id: number;
   name: string;
   phoneNumber: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestPhonebookAction {
   type: 'REQUEST_PHONEBOOK';
}

interface ReceivePhonebookAction {
   type: 'RECEIVE_PHONEBOOK';
   phonebook: Phonebook;
}

interface RequestPhonebookEntriesAction {
   type: 'REQUEST_PHONEBOOK_ENTRIES';
   startIndex: number;
}

interface ReceivePhonebookEntriesAction {
   type: 'RECEIVE_PHONEBOOK_ENTRIES';
   startIndex: number;
   entries: PhonebookEntries[];
}

interface UpdateNewEntryAction {
   type: 'UPDATE_PHONEBOOK_NEW_ENTRY';
   value: any;
}

interface RequestAddNewEntryAction {
   type: 'REQUEST_ADD_PHONEBOOK_NEW_ENTRY';
}

interface ReceiveAddNewEntryAction {
   type: 'RECEIVE_ADD_PHONEBOOK_NEW_ENTRY';
   value: PhonebookEntries;
}

interface EditEntryAction {
   type: 'EDIT_ENTRY';
   id: number;
}

interface RequestSaveEntryAction {
   type: 'REQUEST_SAVE_ENTRY';
}

interface ReceiveSaveEntryAction {
   type: 'RECEIVE_SAVE_ENTRY';
   value: PhonebookEntries;
}

interface RequestDeleteEntryAction {
   type: 'REQUEST_DELETE_ENTRY';
   id: number;
}

interface ReceiveDeleteEntryAction {
   type: 'RECEIVE_DELETE_ENTRY';
   id: number;
}

interface ReceiveErrorAction {
   type: 'RECEIVE_ERROR';
   value: string;
}


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestPhonebookAction | ReceivePhonebookAction | RequestPhonebookEntriesAction
   | ReceivePhonebookEntriesAction | UpdateNewEntryAction | RequestAddNewEntryAction | ReceiveAddNewEntryAction
   | ReceiveErrorAction | EditEntryAction | RequestSaveEntryAction | ReceiveSaveEntryAction
   | RequestDeleteEntryAction | ReceiveDeleteEntryAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
   requestPhonebook: (name: string): AppThunkAction<KnownAction> => async (dispatch, getState) => {

      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.name) {

         dispatch({ type: 'REQUEST_PHONEBOOK' });
         try {
            let response = await fetch(`/api/Phonebook/${appState.phonebook.name}`);
            let phonebook: Phonebook = await response.json();
            dispatch({ type: 'RECEIVE_PHONEBOOK', phonebook });
         } catch (ex) {
            console.log('requestPhonebook ex', ex);
            dispatch({ type: 'RECEIVE_ERROR', value: ex.message });
         }
      }
   },
   requestPhonebookEntries: (startIndex: number): AppThunkAction<KnownAction> => async (dispatch, getState) => {

      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.id && startIndex !== appState.phonebook.startIndex) {

         dispatch({ type: 'REQUEST_PHONEBOOK_ENTRIES', startIndex: startIndex });
         try {
            let response = await fetch(`/api/PhonebookEntry/forPhone/${appState.phonebook.id}`);
            let entries: PhonebookEntries[] = await response.json();
            dispatch({ type: 'RECEIVE_PHONEBOOK_ENTRIES', startIndex, entries  });
         } catch (ex) {
            console.log('requestPhonebook ex', ex);
            dispatch({ type: 'RECEIVE_ERROR', value: ex.message });
         }
      }
   },
   updatePhonebookNewEntryProperty: (value: any): AppThunkAction<KnownAction> => (dispatch, getState) => {
         dispatch({ type: 'UPDATE_PHONEBOOK_NEW_ENTRY', value });      
   },
   addNewEntry: (): AppThunkAction<KnownAction> => async (dispatch, getState) => {
      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.newEntry && appState.phonebook.newEntry.name && appState.phonebook.newEntry.phoneNumber) {
         dispatch({ type: 'REQUEST_ADD_PHONEBOOK_NEW_ENTRY' });
         try {
            let newEntry: any = Object.assign({}, appState.phonebook.newEntry, { phoneBookId: appState.phonebook.id });
            let response = await fetch(`/api/PhonebookEntry/`,
               {
                  headers: {
                     'Accept': 'application/json',
                     'Content-Type': 'application/json'
                  },
                  method: "POST",
                  body: JSON.stringify(newEntry)
               });
            if (response.status == 200) {
               let entry: PhonebookEntries = await response.json();
               dispatch({ type: 'RECEIVE_ADD_PHONEBOOK_NEW_ENTRY', value: entry });
            } else {
               let errors: any[] = await response.json();
               let stringErrors = errors.map((v) => v.description).toString();
               dispatch({ type: 'RECEIVE_ERROR', value: stringErrors });
            }
         } catch (ex) {
            console.log('requestPhonebook ex', ex);
            dispatch({ type: 'RECEIVE_ERROR', value: ex.message });
         }
      }
   },
   editEntry: (id: number): AppThunkAction< KnownAction > => async (dispatch, getState) => {
      dispatch({ type: 'EDIT_ENTRY', id });
   },
   saveEntry: (id: number): AppThunkAction<KnownAction> => async (dispatch, getState) => {
      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.newEntry && appState.phonebook.newEntry.name && appState.phonebook.newEntry.phoneNumber) {
         dispatch({ type: 'REQUEST_SAVE_ENTRY' });
         try {
            let newEntry: any = Object.assign({}, appState.phonebook.newEntry, { phoneBookId: appState.phonebook.id });
            let response = await fetch(`/api/PhonebookEntry/`,
               {
                  headers: {
                     'Accept': 'application/json',
                     'Content-Type': 'application/json'
                  },
                  method: "PUT",
                  body: JSON.stringify(newEntry)
               });
            if (response.status == 200) {
               let entry: PhonebookEntries = await response.json();
               dispatch({ type: 'RECEIVE_SAVE_ENTRY', value: entry });
            } else {
               let errors: any[] = await response.json();
               let stringErrors = errors.map((v) => v.description + '\n').toString();
               dispatch({ type: 'RECEIVE_ERROR', value: stringErrors });
            }
         } catch (ex) {
            console.log('requestPhonebook ex', ex);
            dispatch({ type: 'RECEIVE_ERROR', value: ex.message });
         }
      }
   },
   deleteEntry: (id: number): AppThunkAction<KnownAction> => async (dispatch, getState) => {

   }

};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: PhonebookState = {
   entries: [], id: 0, name: 'Phonebook 1', isLoading: false, startIndex: -1,
   newEntry: {
      id: 0,
      name: '',
      phoneNumber: ''
   },
   errors: ''
};

export const reducer: Reducer<PhonebookState> = (state: PhonebookState | undefined, incomingAction: Action): PhonebookState => {
   if (state === undefined) {
      return unloadedState;
   }

   const action = incomingAction as KnownAction;
   switch (action.type) {
      case 'REQUEST_PHONEBOOK':
         return {
            ...state,
            isLoading: true
         };
      case 'RECEIVE_PHONEBOOK':
         return {
            ...state,
            id: action.phonebook.id,
            name: action.phonebook.name,
            startIndex: -1,
            isLoading: false
         };
      case 'REQUEST_PHONEBOOK_ENTRIES':
         return {
            ...state,
            startIndex: action.startIndex,
            isLoading: true
         };
      case 'RECEIVE_PHONEBOOK_ENTRIES':
         // Only accept the incoming data if it matches the most recent request. This ensures we correctly
         // handle out-of-order responses.
         if (action.startIndex === state.startIndex) {
            return {
               ...state,
               entries: action.entries,
               isLoading: false
            };
         }
         break;
      case 'UPDATE_PHONEBOOK_NEW_ENTRY':
         return {
            ...state,
            newEntry: {
               ...state.newEntry,
               ...action.value
            }
         }
      case 'REQUEST_ADD_PHONEBOOK_NEW_ENTRY':
         return {
            ...state,
            isLoading: true
         };
      case 'RECEIVE_ADD_PHONEBOOK_NEW_ENTRY':
         // Only accept the incoming data if it matches the most recent request. This ensures we correctly
         // handle out-of-order responses.
         if (action.value) {
            return {
               ...state,
               entries: [...state.entries, action.value],
               isLoading: false,
               newEntry: {
                  id: 0,
                  name: '',
                  phoneNumber: ''
               },
               errors: ''
            };
         }
         break;
      case 'RECEIVE_ERROR':
         return {
            ...state,
            isLoading: false,
            errors: action.value
         }
   }

   return state;
};
