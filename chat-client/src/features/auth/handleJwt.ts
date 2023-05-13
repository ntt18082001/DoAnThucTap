import jwt_decode from "jwt-decode";

// Hàm kiểm tra token hết hạn
export const isAccessTokenExpired = (token: string) => {
  const decodedToken: any = jwt_decode(token);
  const expirationTime = decodedToken.exp * 1000; // Đổi về milliseconds
  const now = Date.now();
  return now >= expirationTime;
}

export const isAdmin = (token: string) => {
  const decodedToken: any = jwt_decode(token);
  return decodedToken.role === "Admin";
}