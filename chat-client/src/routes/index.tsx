import { lazy } from 'react';
import { Navigate, useRoutes } from 'react-router-dom';
import { routeAdmin, routeForgotPassword, routeFriends, routeMessage, routeProfile, routeUserAdmin } from '../constants';
import { routeNotFound, routeLogin, routeRegister } from '../constants/index';

const MainLayout = lazy(() => import('components/Layout/Client/MainLayout'));
const HomeLayout = lazy(() => import('components/Layout/Client/HomeLayout'));
const MessageLayout = lazy(() => import('components/Layout/Client/MessageLayout'));
const ProfileLayout = lazy(() => import('components/Layout/Client/ProfileLayout'));
const FriendLayout = lazy(() => import('components/Layout/Client/FriendLayout'));

const MessageContent = lazy(() => import('features/message/pages/MessageContent'));

const LoginPage = lazy(() => import('features/auth/pages/LoginPage'));
const RegisterPage = lazy(() => import('features/auth/pages/RegisterPage'));
const ForgotPasswordPage = lazy(() => import('features/auth/pages/ForgotPassword'));

const Profile = lazy(() => import('features/profile/pages/Profile'));

const FriendShip = lazy(() => import('features/friends/pages/FriendShip'));

const NotFound = lazy(() => import('components/Common/Client/NotFound'));

const AdminLayout = lazy(() => import('components/Layout/Admin/DashboardAdmin'));
const MainAdminLayout = lazy(() => import('features/admin/main/MainAdmin'));
const UserLayout = lazy(() => import('features/admin/user/User'));

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
				},
				{
					path: routeAdmin,
					element: <AdminLayout />,
					children: [
						{
							path: '',
							element: <MainAdminLayout />
						},
						{
							path: routeUserAdmin,
							element: <UserLayout />
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
			path: routeForgotPassword,
			element: <ForgotPasswordPage />
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