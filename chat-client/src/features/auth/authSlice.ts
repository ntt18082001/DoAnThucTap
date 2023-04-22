import { createSelector, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { AuthModel, UserModel } from '../../models/useridentity.model';


const initialState: AuthModel = {
	token: null,
	user: null,
	isLoggedIn: false
}

export const authSlice = createSlice({
	name: 'auth',
	initialState,
	reducers: {
		setToken: (state, action: PayloadAction<string>) => {
			state.token = action.payload;
			state.isLoggedIn = true;
		},
		setUser: (state, action: PayloadAction<UserModel>) => {
			state.user = action.payload;
		},
		logout: (state) => {
			state.token = null;
			state.user = null;
			state.isLoggedIn = false;
		},
		setAvatar: (state, action: PayloadAction<string>) => {
			if(state.user) {
				state.user.avatar = action.payload;
			}
		}
	}
});

export const { setToken, setUser, logout, setAvatar } = authSlice.actions;

export const selectToken = (state: RootState) => state.auth.token;
export const selectIsLoggedIn = (state: RootState) => state.auth.isLoggedIn;
export const selectUser = (state: RootState) => state.auth.user;
export const selectUserId = (state: RootState) => state.auth.user?.id;

export const getToken = createSelector(
  (state: RootState) => state.auth,
  (auth) => auth.token
);

export default authSlice.reducer;