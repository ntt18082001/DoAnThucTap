import { Container, LinearProgress } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsLoggedIn } from 'features/auth/authSlice';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React, { Suspense, useEffect } from 'react'
import { Outlet, useNavigate } from 'react-router-dom';

function FriendLayout() {
  const darkmode = useAppSelector(selectIsDarkmode);
	const isLoggedIn = useAppSelector(selectIsLoggedIn);
  const navigate = useNavigate();

	useEffect(() => {
		document.title = "Gợi ý kết bạn";
		if(!isLoggedIn) {
			navigate("/");
		}
	}, [isLoggedIn, navigate]);
  
  return (
    <>
      <Container
        className={darkmode ? 'darkmode-bg-color' : ''}
        sx={{ pt: 4 }}
      >
        <Suspense fallback={<LinearProgress color="secondary" />}>
          <Outlet />
        </Suspense>
      </Container>
    </>
  );
}

export default React.memo(FriendLayout);