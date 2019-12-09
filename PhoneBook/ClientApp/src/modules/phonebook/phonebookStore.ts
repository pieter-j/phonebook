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
   startIndex: number;
}

export interface PhonebookEntries {
   id: number;
   name: string;
   PhoneNumber: string;
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

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestPhonebookAction | ReceivePhonebookAction | RequestPhonebookEntriesAction | ReceivePhonebookEntriesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
   requestPhonebook: (name: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.name) {
         fetch(`/api/Phonebook/${appState.phonebook.name}`)
            .then(response => response.json() as Promise<Phonebook>)
            .then(data => {
               dispatch({ type: 'RECEIVE_PHONEBOOK', phonebook: data });
            });

         dispatch({ type: 'REQUEST_PHONEBOOK' });
      }
   },
   requestPhonebookEntries: (startIndex: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
      const appState = getState();
      if (appState && appState.phonebook && appState.phonebook.id && startIndex !== appState.phonebook.startIndex) {
         fetch(`/api/PhonebookEntry`)
            .then(response => response.json() as Promise<PhonebookEntries[]>)
            .then(data => {
               dispatch({ type: 'RECEIVE_PHONEBOOK_ENTRIES', startIndex: startIndex, entries: data });
            });

         dispatch({ type: 'REQUEST_PHONEBOOK_ENTRIES', startIndex: startIndex });
      }
   }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: PhonebookState = { entries: [], id: 0, name: 'Phonebook 1', isLoading: false, startIndex: -1 };

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
   }

   return state;
};
