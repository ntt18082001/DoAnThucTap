import { HubConnection } from '@microsoft/signalr';
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';

interface Hub {
	hubConnection: HubConnection | null;
}

const initialState: Hub = {
	hubConnection: null
};

export const hubSlice = createSlice({
	name: 'hub',
	initialState,
	reducers: {
		setHubConnection: (state, action: PayloadAction<HubConnection>) => {
			state.hubConnection = action.payload;
		}
	}
});

export const { setHubConnection } = hubSlice.actions;

export const selectHub = (state: RootState) => state.hub.hubConnection;

export default hubSlice.reducer;