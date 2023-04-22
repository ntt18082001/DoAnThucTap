import { Middleware } from "@reduxjs/toolkit";
import { RootState } from "app/store";
import { logout } from "features/auth/authSlice";
import { isAccessTokenExpired } from "features/auth/handleJwt";
import { toast } from "react-toastify";

// Middleware để kiểm tra token
export const validateTokenMiddleware: Middleware = (store) => (next) => (action) => {
  // Thực hiện action
  const result = next(action);

  // Kiểm tra token sau mỗi action
  const state = store.getState();
  const { token } = state.auth;
  if (token) {
    // Nếu token đã hết hạn, dispatch action để đăng xuất người dùng
    if (isAccessTokenExpired(token)) {
      toast.info("Đã hết thời gian đăng nhập!");
      store.dispatch(logout());
    }
  }

  return result;
};