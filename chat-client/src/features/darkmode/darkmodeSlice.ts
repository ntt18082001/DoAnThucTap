import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';

export interface DarkmodeState {
	isDarkmode: boolean;
}

const initialState: DarkmodeState = {
	isDarkmode: false
}

export const darkmodeSlice = createSlice({
	name: 'darkmode',
	initialState,
	reducers: {
		setDarkmode: (state, action: PayloadAction<boolean>) => {
			state.isDarkmode = action.payload;
		},
		updateDarkmode: (state) => {
			state.isDarkmode = !state.isDarkmode;
			localStorage.setItem("darkmode", JSON.stringify(state.isDarkmode));
		}
	}
});

export const { updateDarkmode, setDarkmode } = darkmodeSlice.actions;

export const selectIsDarkmode = (state: RootState) => state.darkmode.isDarkmode;

export default darkmodeSlice.reducer;