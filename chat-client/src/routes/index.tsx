import { lazy } from 'react';
import { Navigate, useRoutes, RouteProps } from 'react-router-dom';
import { routeFriends, routeMessage, routeProfile } from '../constants';
import { routeNotFound, routeLogin, routeRegister } from '../constants/index';

const MainLayout = lazy(() => import('components/Layout/MainLayout'));
const HomeLayout = lazy(() => import('components/Layout/HomeLayout'));
const MessageLayout = lazy(() => import('components/Layout/MessageLayout'));
const ProfileLayout = lazy(() => import('components/Layout/ProfileLayout'));
const FriendLayout = lazy(() => import('components/Layout/FriendLayout'));

const MessageContent = lazy(() => import('features/message/pages/MessageContent'));

const LoginPage = lazy(() => import('features/auth/pages/LoginPage'));
const RegisterPage = lazy(() => import('features/auth/pages/RegisterPage'));

const Profile = lazy(() => import('features/profile/pages/Profile'));

const FriendShip = lazy(() => import('features/friends/pages/FriendShip'));

const NotFound = lazy(() => import('components/Common/NotFound'));

const Routing: React.FC = (): JSX.Element => {
	const routing = useRoutes([
		{
			path: '/',
			element: <MainLayout />,
			children: [
				{ path: '*', element: <Navigate to={`/${routeNotFound}`} /> },
				{ path: '/', element: <HomeLayout /> },
				{
					path: routeMessage,
					element: <MessageLayout />,
					children: [
						{ path: ':id', element: <MessageContent /> }
					]
				},
				{
					path: routeProfile,
					element: <ProfileLayout />,
					children: [
						{
							path: ':id',
							element: <Profile />
						}
					]
				},
				{
					path: routeFriends,
					element: <FriendLayout />,
					children: [
						{
							path: '',
							element: <FriendShip />
						}
					]
				}
			]
		},
		{
			path: routeLogin,
			element: <LoginPage />
		},
		{
			path: routeRegister,
			element: <RegisterPage />
		},
		{
			path: routeNotFound,
			element: <NotFound />
		}
	]);

	return (
		<>
			{routing}
		</>
	);
}

export default Routing;