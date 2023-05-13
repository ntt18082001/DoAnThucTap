import { Container, LinearProgress } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsLoggedIn } from 'features/auth/authSlice';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React, { Suspense, useEffect } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';

function ProfileLayout() {
	const darkmode = useAppSelector(selectIsDarkmode);
	const isLoggedIn = useAppSelector(selectIsLoggedIn);
  const navigate = useNavigate();
	useEffect(() => {
		document.title = "Trang cá nhân";
		if(!isLoggedIn) {
			navigate("/");
		}
	}, [isLoggedIn, navigate]);
  return (
    <>
      <Container
        className={darkmode ? 'darkmode-bg-color' : ''}
      >
        <Suspense fallback={<LinearProgress color="secondary" />}>
          <Outlet />
        </Suspense>
      </Container>
    </>
  );
}

export default React.memo(ProfileLayout);
