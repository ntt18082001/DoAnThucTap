import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../../app/store';
import { NotifyModel } from 'models/notify.model';

export interface NotifyState {
  listNotify: NotifyModel[];
}

const initialState: NotifyState = {
  listNotify: []
}

const notifySlice = createSlice({
  name: 'notify',
  initialState,
  reducers: {
    setListNotify: (state, action: PayloadAction<NotifyModel[]>) => {
      state.listNotify = action.payload;
    },
    setNotify: (state, action: PayloadAction<NotifyModel>) => {
      state.listNotify.unshift(action.payload);
    },
    setCancelNotify: (state, action: PayloadAction<string>) => {
      const indexNotify = state.listNotify.findIndex(x => x.id === action.payload);
      if (indexNotify !== -1) {
        state.listNotify[indexNotify].isCancel = true;
      }
    },
    cancelNotifyFromSender: (state, action: PayloadAction<string>) => {
      const indexNotify = state.listNotify.findIndex(x => x.id === action.payload);
      if (indexNotify !== -1) {
        state.listNotify.splice(indexNotify, 1);
      }
    },
    setAcceptRequestNotify: (state, action: PayloadAction<string>) => {
      const indexNotify = state.listNotify.findIndex(x => x.id === action.payload);
      if (indexNotify !== -1) {
        state.listNotify[indexNotify].isAccept = true;
      }
    }
  }
});

export const { setListNotify, setNotify, setCancelNotify, cancelNotifyFromSender, setAcceptRequestNotify } = notifySlice.actions;

export const selectListNotify = (state: RootState) => state.notify.listNotify;
export const selectLengthNotify = (state: RootState) => state.notify.listNotify.filter(x => x.isAccept === false && x.isCancel === false).length;

export default notifySlice.reducer;